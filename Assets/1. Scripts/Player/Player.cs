using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private GameObject obj;
    [SerializeField] private GameObject cart;
    [SerializeField] private Transform cartTransform;
    [SerializeField] private int maxObjStackCount = 5;

    private float speed = 5f;
    private float cartSpeed = 2.5f;

    private CharacterController cc;
    private Animator animator;

    private Stack<GameObject> ingredientStack = new Stack<GameObject>();
    private Stack<GameObject> churuStack = new Stack<GameObject>();
    private Stack<GameObject> boxStack = new Stack<GameObject>();
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        JoystickMove();
        OnCart();
    }

    private void JoystickMove()
    {
        if (joystick == null)
        {
            joystick = FindObjectOfType<Joystick>();
            return;
        }

        float joyX = joystick.Horizontal;
        float joyZ = joystick.Vertical;

        Vector3 moveDirection = new Vector3(joyX, 0, joyZ);

        if (moveDirection != Vector3.zero)
        {
            Vector3 cameraForward = mainCamera.transform.forward;
            cameraForward.y = 0;
            cameraForward.Normalize();

            Vector3 cameraRight = mainCamera.transform.right;
            cameraRight.y = 0;
            cameraRight.Normalize();

            Vector3 adjustedDirection = (moveDirection.z * cameraForward + moveDirection.x * cameraRight).normalized;

            float currentSpeed = animator.GetFloat("Blend") == 1? cartSpeed : speed;
            animator.SetBool("isMove", true);
            if (ingredientStack.Count > 0 || BoxStack.Count > 0)
            {
                animator.SetFloat("Blend", 1);
            }
            else
            {
                animator.SetFloat("Blend", 0);
            }

            cc.Move(adjustedDirection * currentSpeed * Time.deltaTime);

            Quaternion newRotation = Quaternion.LookRotation(adjustedDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10f);
        }
        else
        {
            animator.SetBool("isMove", false);
        }

        Vector3 currentPosition = transform.position;
        currentPosition.y = 0;
        transform.position = currentPosition;
    }

    public void OnCart()
    {
        if (ingredientStack.Count <= 0 && boxStack.Count <= 0)
        {
            cart.transform.DOScale(0, 0.2f);
            
        }
        else
        {
            cart.transform.DOScale(1, 0.2f);
        }
    }

    public void TakeObject(IngredientMaker im)
    {
        if (im.ChuruStack.Count > 0 && maxObjStackCount > ingredientStack.Count && boxStack.Count <= 0)
        {
            Utility.ObjectDrop(cartTransform, null, im.ChuruStack, ingredientStack, 1);
        }
    }
    public void GiveObject(ConveyorBelt cb, bool isChuru)
    {
        Stack<GameObject> stack = new Stack<GameObject>();

        if (isChuru) stack = churuStack;
        else stack = ingredientStack;

        if (ingredientStack.Count > 0 || churuStack.Count > 0)
        {
            Utility.ObjectDrop(cb.IngredientStorage, null, stack, cb.CbStack, 1);
        }
    }
    public void GiveObject(BoxStorage bs, bool isChuru)
    {
        Stack<GameObject> stack = new Stack<GameObject>();
        if (isChuru)  stack = churuStack;
        else stack = boxStack;

        if (bs.BoxStack.Count > 0  && ingredientStack.Count <= 0)
        {
            Utility.ObjectDrop(cartTransform, null, bs.BoxStack, stack, 1);
        }
    }
    public void GiveObject(Delivery dv)
    {
        if (dv.DeliveryStack.Count > 0 && ingredientStack.Count <= 0)
        {
            Utility.ObjectDrop(cartTransform, null, dv.DeliveryStack, boxStack, 1);
        }
    }
    public void GiveObject(TestCar tc)
    {
        if (boxStack.Count > 0 && ingredientStack.Count <= 0)
        {
            Utility.ObjectDrop(tc.gameObject.transform, null, boxStack, tc.BoxStack, 3);
        }
    }
}
