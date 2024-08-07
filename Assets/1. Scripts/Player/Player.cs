using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    private float speed = 5f;

    [SerializeField] private Joystick joystick;
    [SerializeField] private CharacterController cc;
    [SerializeField] private GameObject obj;
    [SerializeField] private GameObject cart;
    [SerializeField] private Transform cartTransform;

    private Stack<GameObject> boxStack = new Stack<GameObject>();
    private Stack<GameObject> ingredientStack = new Stack<GameObject>();
    public Stack<GameObject> ObjStack
    {
        get { return ingredientStack; }
        set
        {
            ingredientStack = value;
        }
    }
    [SerializeField] private int maxObjStackCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
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

        cc.Move(moveDirection * speed * Time.deltaTime);

        if(moveDirection != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
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
    public void TakeObject(ChuruManager churu)
    {
        if (churu.ChuruStack.Count > 0 && maxObjStackCount > ingredientStack.Count && boxStack.Count <= 0)
        {
            Utility.ObjectDrop(cartTransform, null, churu.ChuruStack, ingredientStack, 1);
        }
    }
    public void GiveObject(ConveyorBelt cb)
    {
        if (ingredientStack.Count > 0)
        {
            Utility.ObjectDrop(cb.IngredientStorage, null, ingredientStack, cb.CbStack, 1);
        }
    }public void GiveObject(BoxStorage bs)
    {
        if (bs.BoxStack.Count > 0 && maxObjStackCount > boxStack.Count && ingredientStack.Count <= 0)
        {
            Utility.ObjectDrop(cartTransform, null, bs.BoxStack, boxStack, 1);
        }
    }
}
