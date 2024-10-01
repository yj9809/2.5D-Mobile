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
    [SerializeField] private int maxObjStackCount = 0;

    [SerializeField] private Transform boxTrans;
    [SerializeField] private Transform truckTrans;

    [SerializeField] private int randomTarget = 0;

    [EnumToggleButtons, SerializeField] private EmployeeType employeeType = EmployeeType.Cart;

    [SerializeField] private bool moving = false;
    private bool isWaiting = false;

    private GameManager gm;
    private Animator animator;
    private NavMeshAgent na;
    [SerializeField] private Transform target;
    private BaseCost baseCost;

    Vector3 previousPosition;
    Vector3 currentPosition;

    public int MaxObjStackCount
    {
        get { return baseCost.employeeBaseMaxObjStackCount; }
        set { baseCost.employeeBaseMaxObjStackCount = value; }
    }

    private Stack<GameObject> ingredientStack = new Stack<GameObject>();
    public Stack<GameObject> IngredientStack
    {
        get { return ingredientStack; }
        set { ingredientStack = value; }
    }

    private Stack<GameObject> churuStack = new Stack<GameObject>();
    public Stack<GameObject> ChuruStack
    {
        get { return churuStack; }
        set { churuStack = value; }
    }

    private Stack<GameObject> boxStack = new Stack<GameObject>();
    public Stack<GameObject> BoxStack
    {
        get { return boxStack; }
        set { boxStack = value; }
    }
    
    [SerializeField] private IStackable currentTarget;

    private void Start()
    {
        gm = GameManager.Instance;
        boxTrans = GameObject.Find("Box Packaging").transform.GetChild(0);
        animator = GetComponent<Animator>();
        na = GetComponent<NavMeshAgent>();
        baseCost = DataManager.Instance.baseCost;

        StartCoroutine(CheckStack());
    }

    private void Update()
    {
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

    private void Move()
    {
        if (!isWaiting)
        {
            bool isBlend = false;

            if (ingredientStack.Count > 0 || churuStack.Count > 0 || boxStack.Count > 0)
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
        if (ingredientStack.Count <= 0 && boxStack.Count <= 0 && churuStack.Count <= 0)
        {
            na.speed = baseCost.employeeBaseSpeed;
            cart.transform.DOScale(0, 0.2f);
        }
        else
        {
            na.speed = baseCost.employeeBaseCartSpeed;
            cart.transform.DOScale(Vector3.one, 0.2f);
        }
    }
    // 타겟 전환용 함수
    private void TargetSwitching()
    {
        Debug.Log(Vector3.Distance(transform.position, target.position));
        if (target != null && (Vector3.Distance(transform.position, target.position) < 1.3f || 
            (ingredientStack.Count >= MaxObjStackCount || churuStack.Count >= MaxObjStackCount || boxStack.Count >= MaxObjStackCount)))
        {
            ChangeTarget();
        }
        else if (currentTarget != null && currentTarget.GetStackCount() == 0 && (ingredientStack.Count <= 0 && churuStack.Count <= 0 && boxStack.Count <= 0))
        {
            // 스택 카운터가 0인 경우 새로운 목표를 설정
            gm.SetTargetBeingUsed(currentTarget, false);
            currentTarget = null;
            moving = false;
            StartCoroutine(CheckStack()); // 목표 재설정
        }
    }
    private void ChangeTarget()
    {
        if (ingredientStack.Count > 0)
        {
            if (currentTarget != null)
            {
                gm.AddTarget(currentTarget); // 대상이 없으면 추가
                gm.SetTargetBeingUsed(currentTarget, false);
            }

            currentTarget = null;
            target = gm.cbTrans[randomTarget];
        }
        else if (churuStack.Count > 0)
        {
            if (currentTarget != null)
            {
                gm.AddTarget(currentTarget); // 대상이 없으면 추가
                gm.SetTargetBeingUsed(currentTarget, false);
            }

            currentTarget = null;
            target = boxTrans;
        }
        else if (boxStack.Count > 0)
        {
            if (currentTarget != null)
            {
                gm.AddTarget(currentTarget); // 대상이 없으면 추가
                gm.SetTargetBeingUsed(currentTarget, false);
            }

            currentTarget = null;
            target = truckTrans;
        }
        else
        {
            moving = false;
            StartCoroutine(CheckStack());
        }
    }
    // 스택 카운터를 판별해 적절한 타겟을 찾아주는 함수
    public IEnumerator CheckStack()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);

            if (!moving)
            {
                IStackable bestTarget = null;
                int highestStackCount = 0;

                foreach (var item in gm.stackCount)
                {
                    // 타겟이 사용 중이지 않은 것만 고려
                    if (!gm.IsTargetBeingUsed(item))
                    {
                        int count = item.GetStackCount();

                        if (count > highestStackCount && (bestTarget == null || count > highestStackCount * 2))
                        {
                            highestStackCount = count;
                            bestTarget = item;
                        }
                    }
                }

                if (bestTarget != null)
                {
                    target = bestTarget.GetTransform();
                    randomTarget = Random.Range(0, gm.cbTrans.Count);
                    currentTarget = bestTarget;
                    moving = true;

                    if (currentTarget != null)
                    {
                        gm.AddTarget(currentTarget); // 대상이 없으면 추가
                        gm.SetTargetBeingUsed(currentTarget, false);
                    }
                    
                    gm.UpdateTargets(); // 모든 종업원에게 타겟 업데이트
                }
            }
        }
    }
    // 재료 받아오는 함수
    public void TakeObject(IngredientMaker im)
    {
        if (im.ChuruStack.Count > 0 && maxObjStackCount > ingredientStack.Count && boxStack.Count <= 0)
        {
            Utility.ObjectDrop(cartTransform, null, im.ChuruStack, ingredientStack, 1);
        }
    }
    // 컨베이어로 옮기는 함수
    public void GiveObject(ConveyorBelt cb)
    {
        if (ingredientStack.Count > 0)
        {
            Utility.ObjectDrop(cb.IngredientStorage, null, ingredientStack, cb.CbStack, 1);
        }
    }
    // 변환 재료 받아오는 함수
    public void GiveObject(BoxStorage bs, bool isChuru)
    {
        Stack<GameObject> newStack = isChuru ? churuStack : boxStack;

        if (bs.BoxStack.Count > 0 && maxObjStackCount > newStack.Count && ingredientStack.Count <= 0)
        {
            Utility.ObjectDrop(cartTransform, null, bs.BoxStack, newStack, 1);
        }
    }
    // 박스 포장대에 옮기는 함수
    public void GiveObject(BoxPackaging bp)
    {
        if (churuStack.Count > 0)
        {
            Utility.ObjectDrop(bp.churuStorageParent, null, churuStack, bp.ChuruStorage, 4);
        }
    }
    public void PackaingEmployee()
    {
        employeeType = EmployeeType.Packaing;
    }
    public void DoBoxPackagingAnimationEmployee()
    {
        transform.rotation = Quaternion.Euler(0, -90f, 0);
        animator.SetLayerWeight(1, 1);
    }

    public void StopBoxPackagingAnimationEmployee()
    {
        animator.SetLayerWeight(1, 0);
    }
}
