using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Joystick joystick;

    private const float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float joyX = joystick.Horizontal;
        float joyZ = joystick.Vertical;

        transform.position += new Vector3(joyX, 0, joyZ) * speed * Time.deltaTime;
    }
}
