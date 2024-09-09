using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public enum PlayerType
{
    Joystick,
    Auto
}
public class Player : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private GameObject obj;
    [SerializeField] private GameObject cart;
    [SerializeField] private Transform cartTransform;

    [SerializeField] private GameObject[] Test;

    private PlayerType pT = PlayerType.Joystick;
    public PlayerType PT
    {
        get { return pT; }
        set { pT = value; }
    }
    [SerializeField] private int maxObjStackCount = 5;
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float cartSpeed = 2.5f;
    [SerializeField] private int gold = 0;

    public int MaxObjStackCount 
    {
        get { return maxObjStackCount; }
        set { maxObjStackCount = value; }
    }
    public float BaseSpeed
    {
        get { return baseSpeed; }
        set { baseSpeed = value; }
    }
    public float CartSpeed
    {
        get { return cartSpeed; }
        set { cartSpeed = value; }
    }
    public int Gold
    {
        get { return gold; }
        set { gold = value; }
    }

    private CharacterController cc;
    private Animator animator;
    private Camera mainCamera;

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

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;
        Vibration.Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(pT == PlayerType.Joystick)
            JoystickMove();

        OnCart();

        if (Input.GetKeyDown(KeyCode.F2))
        {
            Instantiate(Test[UnityEngine.Random.Range(0, Test.Length)], Vector3.zero, Quaternion.identity);
        }
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

            float currentSpeed = animator.GetFloat("Blend") == 1? cartSpeed : baseSpeed;
            animator.SetBool("isMove", true);

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
    public void PlayerAutoMove(Transform pos, Action action)
    {
        if(ingredientStack.Count <=0 && boxStack.Count <=0 && churuStack.Count <= 0)
        {
            pT = PlayerType.Auto;
            animator.SetBool("isMove", true);

            float distance = Vector3.Distance(transform.position, pos.position);
            float moveDuration = distance / baseSpeed;

            Vector3 direction = (pos.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.DOMove(pos.position, moveDuration).SetEase(Ease.Linear).OnUpdate(() =>
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
            }).OnComplete(() =>
            {
                animator.SetBool("isMove", false);
                action();
            });
        }
    }

    public void OnCart()
    {
        if (ingredientStack.Count <= 0 && boxStack.Count <= 0 && churuStack.Count <= 0)
        {
            cart.transform.DOScale(0, 0.2f);
            animator.SetFloat("Blend", 0);
        }
        else
        {
            cart.transform.DOScale(1, 0.2f);
            animator.SetFloat("Blend", 1);
        }
    }

    public void DoBoxPackagingAnimation()
    {
        transform.rotation = Quaternion.Euler(0, -90f, 0);
        animator.SetLayerWeight(1, 1);
    }

    public void StopBoxPackagingAnimation()
    {
        animator.SetLayerWeight(1, 0);
    }

    public void TakeObject(IngredientMaker im)
    {
        if (im.ChuruStack.Count > 0 && maxObjStackCount > ingredientStack.Count && boxStack.Count <= 0)
        {
            Utility.ObjectDrop(cartTransform, null, im.ChuruStack, ingredientStack, 1);
            Vibration.VibratePop();
        }
    }
    public void GiveObject(ConveyorBelt cb)
    {
        if (ingredientStack.Count > 0)
        {
            Utility.ObjectDrop(cb.IngredientStorage, null, ingredientStack, cb.CbStack, 1);
            Vibration.VibratePop();
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
            Vibration.VibratePop();
        }
    }

}
