using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Employee : MonoBehaviour
{
    [SerializeField] private Transform ingredientPoint;
    [SerializeField] private Transform conveyorBletPoint;

    [SerializeField] private GameObject cart;
    [SerializeField] private Transform cartTransform;
    [SerializeField] private int maxObjStackCount = 5;

    [SerializeField] private float speed = 2f;
    [SerializeField] private float waitTime = 2f;
    private bool moving = false;
    private bool isWaiting = false;

    private Animator animator;

    private Stack<GameObject> ingredientStack = new Stack<GameObject>();
    public Stack<GameObject> IngredientStack
    {
        get { return ingredientStack; }
        set
        {
            ingredientStack = value;
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
        //transform.position = ingredientPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        OnCart();
    }

    private void Move()
    {
        if (!isWaiting)
        {
            Vector3 targetPosition;

            if (moving)
            {
                targetPosition = conveyorBletPoint.position;
            }
            else
            {
                targetPosition = ingredientPoint.position;
            }

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                moving = !moving;
                StartCoroutine(WaitAtPosition());
            }

            animator.SetBool("isWalk", true);

            Vector3 direction = targetPosition - transform.position;
            direction.y = 0;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
        else
        {
            // 대기 상태일 때 애니메이션을 정지 상태로 설정
            animator.SetBool("isWalk", false);
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
        if (ingredientStack.Count <= 0 && boxStack.Count <= 0)
        {
            cart.transform.DOScale(0, 0.2f);
        }
        else
        {
            cart.transform.DOScale(new Vector3(1, 0.01f, 2), 0.2f);
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
}
