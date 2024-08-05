using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private GameObject obj;
    [SerializeField] private Transform testTrans;
    [SerializeField] private ChuruManager churu;
    private CharacterController cc;

    private Stack<GameObject> testList = new Stack<GameObject>();
    private const float speed = 5f;
    private float time = 0;

    [SerializeField] private GameObject chu;
    // Start is called before the first frame update
    void Start()
    {
        time = float.MaxValue;
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
        time += Time.deltaTime;
        JoystickMove();

        if(chu != null)
        {
            Vector3 pos = new Vector3(testTrans.position.x, testTrans.position.y + testList.Count * Utility.ObjRendererCheck(chu), testTrans.position.z);
            chu.transform.position = Vector3.Slerp(chu.transform.position, pos, 0.01f);
        }
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
            if (churu.Churu.Count > 0)
            {
                chu = churu.Churu.Pop();
                chu.transform.parent = testTrans;
                testList.Push(chu);
                time = 0;
            }
        }
    }
}
