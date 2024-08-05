using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private GameObject obj;
    [SerializeField] private Transform testTrans;
    [SerializeField] private ChuruManager churu;
    private CharacterController cc;

    private Stack<GameObject> testList = new Stack<GameObject>();
    private const float speed = 5f;

    [SerializeField] private GameObject chu;
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
        Vector3 pos = new Vector3(testTrans.position.x, testTrans.position.y + testList.Count * Utility.ObjRendererCheck(chu), testTrans.position.z);
        if (chu != null)
        {
            chu.transform.DOMove(pos, 0.1f).SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    Vector3 setPos = new Vector3(0, pos.y, 0);
                    GameObject newChu = Instantiate(chu);
                    newChu.transform.position = setPos;
                    newChu.transform.SetParent(testTrans);
                    chu = null;
                });
        }
    }
    private void FixedUpdate()
    {
       
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
            if (churu.Churu.Count > 0 && chu == null)
            {
                chu = churu.Churu.Pop();
                chu.transform.parent = testTrans;
                testList.Push(chu);
            }
        }
    }
}
