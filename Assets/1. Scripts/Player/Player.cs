using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private GameObject obj;
    [SerializeField] private Transform testTrans;
    private CharacterController cc;

    private Stack<GameObject> testList = new Stack<GameObject>();
    private const float speed = 5f;
    private float time = 0;

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
        time += Time.deltaTime;
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
            if (churu.Churu.Count > 0 && time >= 0.3)
            {
                GameObject newChuru = churu.Churu.Pop();
                Vector3 pos = new Vector3(testTrans.position.x, testTrans.position.y + testList.Count * Utility.ObjRendererCheck(newChuru), testTrans.position.z);
                newChuru.transform.position = Vector3.Slerp(newChuru.transform.position, pos, 0.5f);
                newChuru.transform.parent = testTrans;
                testList.Push(newChuru);
                time = 0;
            }
        }
    }
}
