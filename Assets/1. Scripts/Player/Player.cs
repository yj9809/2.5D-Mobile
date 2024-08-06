using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    private float speed = 5f;

    [SerializeField] private Joystick joystick;
    [SerializeField] private GameObject obj;
    [SerializeField] private CharacterController cc;
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
        if (joystick == null)
        {
            Debug.LogError("Joystick이 빠져있습니다.");
            return;
        }
        JoystickMove();
    }
    private void JoystickMove()
    {
        float joyX = joystick.Horizontal;
        float joyZ = joystick.Vertical;

        cc.Move(new Vector3(joyX, 0, joyZ) * speed * Time.deltaTime);
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
