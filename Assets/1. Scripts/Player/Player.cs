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
}
