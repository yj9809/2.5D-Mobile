using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class Employee : MonoBehaviour
{
    [SerializeField] private GameObject cart;
    [SerializeField] private Transform cartTransform;
    [SerializeField] private int maxObjStackCount = 5;

    [SerializeField] private Transform boxTrans;
    [SerializeField] private Transform truckTrans;

    [SerializeField] private float speed = 3f;
    [SerializeField] private float cartSpeed = 1.5f;
    [SerializeField] private float waitTime = 1f;
    [SerializeField] private int randomTarget = 0;

    [SerializeField] private bool moving = false;
    private bool isWaiting = false;

    private GameManager gm;
    private Animator animator;
    private NavMeshAgent na;
    private Transform target;

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
        animator = GetComponent<Animator>();
        na = GetComponent<NavMeshAgent>();
        StartCoroutine(CheckStack());
    }

    private void Update()
    {
        Move();
        OnCart();

        if (target != null)
            na.SetDestination(target.position);

        if (target != null && Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            if (ingredientStack.Count > 0)
            {
                gm.SetTargetBeingUsed(currentTarget, false);
                currentTarget = null;
                target = gm.cbTrans[randomTarget];
            }
            else if (churuStack.Count > 0)
            {
                gm.SetTargetBeingUsed(currentTarget, false);
                currentTarget = null;
                target = boxTrans;
            }
            else if (boxStack.Count > 0)
            {
                gm.SetTargetBeingUsed(currentTarget, false);
                currentTarget = null;
                target = truckTrans;
            }
            else
            {
                moving = false;
                StartCoroutine(CheckStack());
            }
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

    private void Move()
    {
        if (!isWaiting)
        {
            float currentSpeed = animator.GetFloat("Blend") == 1 ? cartSpeed : speed;
            animator.SetBool("isMove", true);
            animator.SetFloat("Blend", ingredientStack.Count > 0 ? 1 : 0);
        }
        else
        {
            animator.SetBool("isMove", false);
        }
    }

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
                    gm.SetTargetBeingUsed(bestTarget, true); // 타겟을 사용 중으로 설정
                    gm.UpdateTargets(); // 모든 종업원에게 타겟 업데이트
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
        Stack<GameObject> newStack = isChuru ? churuStack : boxStack;

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
