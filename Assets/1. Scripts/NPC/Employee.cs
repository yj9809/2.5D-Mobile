using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public enum EmployeeType { im, cb}

public class Employee : MonoBehaviour
{
    [SerializeField] private GameObject cart;
    [SerializeField] private Transform cartTransform;
    [SerializeField] private int maxObjStackCount = 5;

    [SerializeField] private Transform cbTrans;
    [SerializeField] private Transform boxTrans;

    [SerializeField] private float speed = 3f;
    [SerializeField] private float cartSpeed = 1.5f;
    [SerializeField] private float waitTime = 1f;
    private bool moving = false;
    private bool isWaiting = false;

    private Animator animator;
    private NavMeshAgent na;
    [SerializeField] private Transform target;
    private EmployeeType employeeType;

    int stackCount = 0;

    private Stack<GameObject> ingredientStack = new Stack<GameObject>();
    public Stack<GameObject> IngredientStack
    {
        get { return ingredientStack; }
        set
        {
            ingredientStack = value;
        }
    }
    private Stack<GameObject> churuStack = new Stack<GameObject>();
    public Stack<GameObject> ChuruStack
    {
        get { return churuStack; }
        set
        {
            churuStack = value;
        }
    }

    private Stack<GameObject> boxStack = new Stack<GameObject>();
    public Stack<GameObject> BoxStack
    {
        get { return boxStack; }
        set
        {
            boxStack = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        na = GetComponent<NavMeshAgent>();
        StartCoroutine(CheckStack());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        OnCart();

        if (target != null)
            na.SetDestination(target.position);

        if (ingredientStack.Count > 0 && employeeType == EmployeeType.im)
            target = cbTrans;
        else if (churuStack.Count > 0)
            target = boxTrans;
        else
            moving = false;
    }

    private void Move()
    {
        if (!isWaiting)
        {
            float currentSpeed = animator.GetFloat("Blend") == 1 ? cartSpeed : speed;
            animator.SetBool("isMove", true);
            if (ingredientStack.Count > 0)
            {
                animator.SetFloat("Blend", 1);
            }
            else
            {
                animator.SetFloat("Blend", 0);
            }

        }
        else
        {
            animator.SetBool("isMove", false);
        }
    }

    private IEnumerator CheckStack()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            if (!moving)
            {
                foreach (var item in GameManager.Instance.stackCount)
                {
                    int count = item.GetStackCount();
                    Transform pos = item.GetTransform();
                    int type = item.GetTypeNum();
                    if (stackCount < count)
                    {
                        stackCount = count;
                        target = pos;
                        employeeType = (EmployeeType)type;
                    }
                }
                if (target != null)
                {
                    moving = true;
                    Debug.Log(moving);
                    stackCount = 0;
                }
            }
        }
    }

    private IEnumerator WaitAtPosition()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
    }

    private void OnCart()
    {
        if (ingredientStack.Count <= 0 && boxStack.Count <= 0 && churuStack.Count <= 0)
        {
            cart.transform.DOScale(0, 0.2f);
        }
        else
        {
            cart.transform.DOScale(Vector3.one, 0.2f);
        }
    }

    public void TakeObject(IngredientMaker im)
    {
        if (im.ChuruStack.Count > 0 && maxObjStackCount > ingredientStack.Count && boxStack.Count <= 0)
        {
            Utility.ObjectDrop(cartTransform, null, im.ChuruStack, ingredientStack, 1);
        }
    }

    public void GiveObject(ConveyorBelt cb)
    {
        if (ingredientStack.Count > 0)
        {
            Utility.ObjectDrop(cb.IngredientStorage, null, ingredientStack, cb.CbStack, 1);
        }
    }
    public void GiveObject(BoxStorage bs, bool isChuru)
    {
        Stack<GameObject> newStack = new Stack<GameObject>();
        newStack = isChuru ? churuStack : boxStack;

        if (bs.BoxStack.Count > 0 && maxObjStackCount > newStack.Count && ingredientStack.Count <= 0)
        {
            Utility.ObjectDrop(cartTransform, null, bs.BoxStack, newStack, 1);
            Vibration.VibratePop();
        }
    }
    public void GiveObject(BoxPackaging bp)
    {
        if (churuStack.Count > 0)
        {
            Utility.ObjectDrop(bp.StorageParent, null, churuStack, bp.ChuruStorage, 4);
            Vibration.VibratePop();
        }
    }
}
