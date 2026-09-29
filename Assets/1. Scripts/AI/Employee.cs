using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using Sirenix.OdinInspector;

public enum EmployeeType { Packaing, Cart}

public class Employee : MonoBehaviour
{
    [SerializeField] private GameObject cart;
    [SerializeField] private Transform cartTransform;

    [SerializeField] private Transform boxTrans;
    [SerializeField] private Transform truckTrans;
    [SerializeField] private Transform target;

    [EnumToggleButtons, SerializeField] private EmployeeType employeeType = EmployeeType.Cart;

    [SerializeField] private bool moving = false;
    private bool isWaiting = false;

    private GameManager gm;
    private Animator animator;
    private NavMeshAgent na;
    private BaseCost baseCost;
    private bool? cartVisible;
    private Tween cartScaleTween;
    private bool compactMode;
    private bool telemetryMoving;
    private CompactSupplyStation compactSupply;
    private CompactManualStation compactManual;
    private CompactSalesCounter compactSales;
    private Transform compactPickupPoint;
    private Transform compactDropoffPoint;
    private Transform compactProcessPoint;

    Vector3 previousPosition;
    Vector3 currentPosition;

    public float MaxObjStackCount
    {
        get { return baseCost.EmployeeMaxStackCount; }
        set { baseCost.EmployeeMaxStackCount = value; }
    }

    private readonly CarrierInventory inventory = new CarrierInventory(0);
    public CarrierInventory Inventory
    {
        get
        {
            // count < a fractional limit permits ceil(limit) items, as before.
            inventory.Capacity = baseCost == null ? 0 : Mathf.Max(0, Mathf.CeilToInt(baseCost.EmployeeMaxStackCount));
            return inventory;
        }
    }

    [SerializeField] private int cbTransNum;
    public int CbTransNum
    {
        get { return cbTransNum; }
        set { cbTransNum = value; }
    }

    private int transportRole;
    private float pickupStarted = -1f;
    public void SetTransportRole(int index) { transportRole = Mathf.Clamp(index, 0, 2); }

    private bool cbTransNumCheck = false;
    public bool CbTransNumCheck
    {
        get { return cbTransNumCheck; }
        set { cbTransNumCheck = value; }
    }

    [SerializeField] private IStackable currentTarget;
    private Coroutine workCheckCoroutine;

    private void Start()
    {
        gm = GameManager.Instance;
        animator = GetComponent<Animator>();
        na = GetComponent<NavMeshAgent>();
        baseCost = DataManager.Instance.baseCost;

        if (TryConfigureCompactWork())
            return;

        try
        {
            var packaging = GameObject.Find("Box Packaging");
            if (packaging != null && packaging.transform.childCount > 0)
                boxTrans = packaging.transform.GetChild(0);
            cbTransNum = gm.cbTrans.Count > 0 ? Random.Range(0, gm.cbTrans.Count) : 0;
        }
        catch(System.Exception err)
        {
            Debug.LogError(err);
        }


        StartWorkCheck();
    }

    private void OnDisable()
    {
        cartScaleTween?.Kill();
        cartVisible = null;
        StopWorkCheck();
        ReleaseCurrentTarget();
    }

    private void Update()
    {
        if (compactMode)
        {
            UpdateCompactWork();
            return;
        }

        if (employeeType == EmployeeType.Packaing)
        {
            cart.SetActive(false);
            return;
        }
        OnCart();
        Move();
        MovementDetection();
        TargetSwitching();

        if (target != null)
            na.SetDestination(target.position);
    }

    public int TransportRole => transportRole;
    public bool IsMovingForTelemetry => telemetryMoving;
    public string RoleLabel => transportRole == 0 ? "원료 운반" :
        transportRole == 1 ? "판매 보충" : "자동 가공";

    private bool TryConfigureCompactWork()
    {
        compactSupply = FindObjectOfType<CompactSupplyStation>();
        compactManual = FindObjectOfType<CompactManualStation>();
        compactSales = FindObjectOfType<CompactSalesCounter>();
        bool compactScene = compactSupply != null || compactManual != null || compactSales != null;
        if (!compactScene)
            return false;

        if (compactSupply == null || compactManual == null || compactSales == null
            || animator == null || na == null || baseCost == null)
        {
            compactMode = true;
            Debug.LogError("Compact employee dependencies are incomplete.", this);
            enabled = false;
            return true;
        }

        foreach (var workPoint in FindObjectsByType<WorkPoint>(
            FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            if (workPoint.Action is CompactManualProcessAction process
                && process.Station == compactManual)
            {
                compactProcessPoint = workPoint.transform;
                continue;
            }

            if (!(workPoint.Action is ItemTransfer transfer)) continue;
            if (transportRole == 0)
            {
                if (transfer.Endpoint == compactSupply) compactPickupPoint = workPoint.transform;
                else if (transfer.Endpoint == compactManual) compactDropoffPoint = workPoint.transform;
            }
            else if (transportRole == 1)
            {
                if (transfer.Endpoint is CompactManualOutput output
                    && output.Station == compactManual)
                    compactPickupPoint = workPoint.transform;
                else if (transfer.Endpoint == compactSales)
                    compactDropoffPoint = workPoint.transform;
            }
        }

        bool routeReady = transportRole == 2
            ? compactProcessPoint != null
            : compactPickupPoint != null && compactDropoffPoint != null;
        if (!routeReady)
        {
            compactMode = true;
            Debug.LogError($"Compact employee route {transportRole} is incomplete.", this);
            enabled = false;
            return true;
        }

        compactMode = true;
        employeeType = EmployeeType.Cart;
        na.stoppingDistance = .05f;
        PlaceOnNavMesh();
        UpdateCompactWork();
        return true;
    }

    private void PlaceOnNavMesh()
    {
        if (na == null || !na.enabled || na.isOnNavMesh) return;
        if (NavMesh.SamplePosition(transform.position, out var hit, 12f, NavMesh.AllAreas))
            na.Warp(hit.position);
    }

    private void UpdateCompactWork()
    {
        OnCart();
        Transform destination = ResolveCompactDestination();
        bool movingToDestination = false;

        if (destination != null && destination.gameObject.activeInHierarchy)
        {
            PlaceOnNavMesh();
            if (na.enabled && na.isOnNavMesh)
            {
                na.isStopped = false;
                na.SetDestination(destination.position);
                float distance = Vector3.Distance(transform.position, destination.position);
                movingToDestination = na.pathPending || distance > Mathf.Max(.18f, na.stoppingDistance + .08f);
                if (!movingToDestination) na.isStopped = true;
            }
        }
        else if (na.enabled && na.isOnNavMesh)
        {
            na.isStopped = true;
        }

        animator.SetBool("isMove", movingToDestination);
        animator.SetFloat("Blend", Inventory.IsEmpty ? 0f : 1f);
        telemetryMoving = movingToDestination;
    }

    private Transform ResolveCompactDestination()
    {
        if (transportRole == 2)
            return compactProcessPoint;

        if (!Inventory.IsEmpty)
        {
            bool destinationHasRoom = transportRole == 0
                ? compactManual.InputCount < compactManual.InputCapacity
                : compactSales.StockCount < compactSales.StockCapacity;
            return destinationHasRoom ? compactDropoffPoint : null;
        }

        bool sourceHasStock = transportRole == 0
            ? compactSupply.StockCount > 0
            : compactManual.OutputCount > 0;
        bool destinationHasCapacity = transportRole == 0
            ? compactManual.InputCount < compactManual.InputCapacity
            : compactSales.StockCount < compactSales.StockCapacity;
        return sourceHasStock && destinationHasCapacity ? compactPickupPoint : null;
    }

    private void Move()
    {
        if (!isWaiting)
        {
            bool isBlend = false;

            if (!Inventory.IsEmpty)
                isBlend = true;

            animator.SetBool("isMove", true);
            animator.SetFloat("Blend", isBlend ? 1 : 0);
        }
        else
        {
            animator.SetBool("isMove", false);
        }
    }
    //이동 판별 함수
    private void MovementDetection()
    {
        currentPosition = GetComponent<CharacterController>().transform.position;

        if (Vector3.Distance(previousPosition, currentPosition) > 0.01f)
            isWaiting = false;
        else
            isWaiting = true;

        previousPosition = currentPosition;
    }
    // 물건을 들고 있는지 판별하는 함수
    private void OnCart()
    {
        bool shouldShowCart = !Inventory.IsEmpty;
        // Keep speed upgrades effective even when the cart visibility stays unchanged.
        na.speed = shouldShowCart ? baseCost.EmployeeCartSpeed : baseCost.EmployeeSpeed;
        if (cartVisible == shouldShowCart)
            return;

        cartVisible = shouldShowCart;
        cartScaleTween?.Kill();
        cartScaleTween = cart.transform.DOScale(shouldShowCart ? 1f : 0f, 0.2f)
            .OnKill(() => cartScaleTween = null);
    }
    // 타겟 전환용 함수
    private void TargetSwitching()
    {
        if (target != null && Vector3.Distance(transform.position, target.position) <= 1.3f)
        {
            ChangeTarget();
        }
        else if (currentTarget != null && currentTarget.GetStackCount() == 0 && Inventory.IsEmpty)
        {
            // 스택 카운터가 0인 경우 새로운 목표를 설정
            ReleaseCurrentTarget();
            moving = false;
            RequestWorkCheck(); // 목표 재설정
        }
    }
    private void ChangeTarget()
    {
        if (currentTarget != null)
        {
            if (pickupStarted < 0f) pickupStarted = Time.time;
            int carried = Inventory.Count;
            // Pick up the stock that already exists without waiting for future production.
            if (carried < MaxObjStackCount && currentTarget.GetStackCount() > 0 && Time.time - pickupStarted < .5f) return;
        }

        if (Inventory.ContainsType(ItemType.Ingredient))
        {
            if (currentTarget != null)
            {
                ReleaseCurrentTarget();
            }

            if(!cbTransNumCheck)
            {
                cbTransNumCheck = true;
                target = gm.ConveyorTransform(this);
            }
        }
        else if (Inventory.ContainsType(ItemType.Churu))
        {
            if (currentTarget != null)
            {
                ReleaseCurrentTarget();
            }

            if(boxTrans == null)
            {
                boxTrans = GameObject.Find("Box Packaging").transform.GetChild(0);
            }

            target = boxTrans;
        }
        else if (Inventory.ContainsType(ItemType.Box))
        {
            if (currentTarget != null)
            {
                ReleaseCurrentTarget();
            }

            target = truckTrans;
        }
        else
        {
            cbTransNumCheck = false;
            moving = false;
            RequestWorkCheck();
        }
    }
    // 스택 카운터를 판별해 적절한 타겟을 찾아주는 함수
    public IEnumerator CheckStack()
    {
        while (true)
        {
            if (!moving)
            {
                if (gm.TryReserveWork(out var bestTarget, transportRole))
                {
                    target = bestTarget.GetTransform();
                    currentTarget = bestTarget;
                    pickupStarted = -1f;
                    moving = true;
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
    // 재료 받아오는 함수
    public void RequestWorkCheck()
    {
        StartWorkCheck();
    }

    private void StartWorkCheck()
    {
        if (workCheckCoroutine == null && isActiveAndEnabled)
        {
            workCheckCoroutine = StartCoroutine(CheckStack());
        }
    }

    private void StopWorkCheck()
    {
        if (workCheckCoroutine == null)
        {
            return;
        }

        StopCoroutine(workCheckCoroutine);
        workCheckCoroutine = null;
    }

    private void ReleaseCurrentTarget()
    {
        if (currentTarget == null)
        {
            return;
        }

        if (gm != null)
        {
            gm.SetTargetBeingUsed(currentTarget, false);
        }

        currentTarget = null;
    }

    public Transform CarryParent => cartTransform;

    public bool CanCollectFrom(IStackable source)
    {
        return ReferenceEquals(currentTarget, source) && transportRole == source.GetTypeNum();
    }

    public void PackaingEmployee()
    {
        cartScaleTween?.Kill();
        cartVisible = null;
        employeeType = EmployeeType.Packaing;
    }
    public void DoBoxPackagingAnimationEmployee()
    {
        transform.rotation = Quaternion.Euler(0, -90f, 0);
        animator.SetLayerWeight(1, 1);
    }
    public void DoManualProcessingAnimation(Vector3 workPosition)
    {
        Vector3 direction = workPosition - transform.position;
        direction.y = 0f;
        if (direction.sqrMagnitude > .001f)
            transform.rotation = Quaternion.LookRotation(direction);
        animator.SetLayerWeight(1, 1);
    }

    public void StopBoxPackagingAnimationEmployee()
    {
        animator.SetLayerWeight(1, 0);
    }
}
