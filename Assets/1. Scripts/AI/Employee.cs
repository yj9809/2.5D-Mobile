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
    //�̵� �Ǻ� �Լ�
    private void MovementDetection()
    {
        currentPosition = GetComponent<CharacterController>().transform.position;

        if (Vector3.Distance(previousPosition, currentPosition) > 0.01f)
            isWaiting = false;
        else
            isWaiting = true;

        previousPosition = currentPosition;
    }
    // ������ ��� �ִ��� �Ǻ��ϴ� �Լ�
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
    // Ÿ�� ��ȯ�� �Լ�
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
            // ���� ī���Ͱ� 0�� ��� ���ο� ��ǥ�� ����
            gm.SetTargetBeingUsed(currentTarget, false);
            currentTarget = null;
            moving = false;
            StartCoroutine(CheckStack()); // ��ǥ �缳��
        }
    }
    private void ChangeTarget()
    {
        if (ingredientStack.Count > 0)
        {
            if (currentTarget != null)
            {
                gm.AddTarget(currentTarget); // ����� ������ �߰�
                gm.SetTargetBeingUsed(currentTarget, false);
            }

            currentTarget = null;
            target = gm.cbTrans[randomTarget];
        }
        else if (churuStack.Count > 0)
        {
            if (currentTarget != null)
            {
                gm.AddTarget(currentTarget); // ����� ������ �߰�
                gm.SetTargetBeingUsed(currentTarget, false);
            }

            currentTarget = null;
            target = boxTrans;
        }
        else if (boxStack.Count > 0)
        {
            if (currentTarget != null)
            {
                gm.AddTarget(currentTarget); // ����� ������ �߰�
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
    // ���� ī���͸� �Ǻ��� ������ Ÿ���� ã���ִ� �Լ�
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
                    // Ÿ���� ��� ������ ���� �͸� ���
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
                        gm.AddTarget(currentTarget); // ����� ������ �߰�
                        gm.SetTargetBeingUsed(currentTarget, false);
                    }
                    
                    gm.UpdateTargets(); // ��� ���������� Ÿ�� ������Ʈ
                }
            }
        }
    }
    // ��� �޾ƿ��� �Լ�
    public void TakeObject(IngredientMaker im)
    {
        if (im.ChuruStack.Count > 0 && maxObjStackCount > ingredientStack.Count && boxStack.Count <= 0)
        {
            Utility.ObjectDrop(cartTransform, null, im.ChuruStack, ingredientStack, 1);
        }
    }
    // �����̾�� �ű�� �Լ�
    public void GiveObject(ConveyorBelt cb)
    {
        if (ingredientStack.Count > 0)
        {
            Utility.ObjectDrop(cb.IngredientStorage, null, ingredientStack, cb.CbStack, 1);
        }
    }
    // ��ȯ ��� �޾ƿ��� �Լ�
    public void GiveObject(BoxStorage bs, bool isChuru)
    {
        Stack<GameObject> newStack = isChuru ? churuStack : boxStack;

        if (bs.BoxStack.Count > 0 && maxObjStackCount > newStack.Count && ingredientStack.Count <= 0)
        {
            Utility.ObjectDrop(cartTransform, null, bs.BoxStack, newStack, 1);
        }
    }
    // �ڽ� ����뿡 �ű�� �Լ�
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
