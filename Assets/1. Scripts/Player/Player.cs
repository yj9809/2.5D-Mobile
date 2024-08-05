using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private CharacterController cc;
    [SerializeField] private GameObject obj;
    [SerializeField] private Transform testTrans;

    private List<GameObject> testList = new List<GameObject>();
    float time = 0;
    private const float speed = 5f;

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
        Debug.Log(time);
        Test();

        JoystickMove();
    }
    private void JoystickMove()
    {
        float joyX = joystick.Horizontal;
        float joyZ = joystick.Vertical;

        cc.Move(new Vector3(joyX, 0, joyZ) * speed * Time.deltaTime);
    }
    private void Test()
    {
        if(time >= 2)
        {
            Debug.Log("실행");
            GameObject obj = Instantiate(this.obj);
            float testHeight = Utility.ObjRendererCheck(obj);
            obj.transform.position = new Vector3(testTrans.position.x, testTrans.position.y + testHeight * testList.Count, testTrans.position.z);
            testList.Add(obj);
            time = 0f;
        }
    }
    
}
