using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

public class BackendManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var bro = Backend.Initialize();

        if(bro.IsSuccess())
        {
            Debug.Log("�ʱ�ȭ ���� : " + bro);
        }
        else
        {
            Debug.LogError("�ʱ�ȭ ���� : " + bro);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
