using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Slerp(transform.position, GameManager.Instance.P.transform.position, 0.01f);   
    }
}
