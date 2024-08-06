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

    private Stack<GameObject> objStack = new Stack<GameObject>();
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
            Debug.LogError("Joystick이 빠져있습니다.");
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
        if(objStack.Count <= 0)
        {
            cart.transform.DOScale(0, 0.2f);
        }
        else
        {
            cart.transform.DOScale(new Vector3(1, 0.01f, 2), 0.2f);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        ChuruManager churu = other.GetComponent<ChuruManager>();
        if (churu != null)
        {
            if (churu.Churu.Count > 0 && maxObjStackCount > objStack.Count)
            {
                Utility.ObjectDrop(cartTransform, null, churu.Churu, objStack, false);
            }
        }
    }
}
