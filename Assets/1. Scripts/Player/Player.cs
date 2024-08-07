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
    private float speed = 5f;

    private int maxObjStackCount = 5;
    private CharacterController cc;

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

    [SerializeField] private int maxObjStackCount = 0;
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
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
            Debug.LogError("Joystick�� �����ֽ��ϴ�.");
            return;
        }

        float joyX = joystick.Horizontal;
        float joyZ = joystick.Vertical;

        Vector3 moveDirection = new Vector3(joyX, 0, joyZ);

        if (moveDirection != Vector3.zero)
        {
            // ī�޶��� ȸ���� �������� moveDirection ��ȯ
            Vector3 cameraForward = mainCamera.transform.forward;
            cameraForward.y = 0;
            cameraForward.Normalize();

            Vector3 cameraRight = mainCamera.transform.right;
            cameraRight.y = 0;
            cameraRight.Normalize();

            Vector3 adjustedDirection = (moveDirection.z * cameraForward + moveDirection.x * cameraRight).normalized;

            cc.Move(adjustedDirection * speed * Time.deltaTime);

            Quaternion newRotation = Quaternion.LookRotation(adjustedDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10f);
        }
    }

    private void OnCart()
    {
        if(ingredientStack.Count <= 0 && boxStack.Count <= 0)
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
    public void GiveObject(BoxStorage bs)
    {
        if (bs.BoxStack.Count > 0 && maxObjStackCount > boxStack.Count && ingredientStack.Count <= 0)
        {
            Utility.ObjectDrop(cartTransform, null, bs.BoxStack, boxStack, 1);
        }
    }
}
