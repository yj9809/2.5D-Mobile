using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBelt : MonoBehaviour
{
    [SerializeField] private Transform nextBelt;
    [SerializeField] private float speed; // 3
    [SerializeField] private Vector3 moveBelt = Vector3.left; // -1
    [SerializeField] private float beltSize; // 2.3

    // Update is called once per frame
    void Update()
    {
        transform.position += moveBelt * speed * Time.deltaTime;

        if (transform.position.x <= -beltSize)
        {
            transform.position = nextBelt.position + Vector3.right * beltSize;
        }
    }
}
