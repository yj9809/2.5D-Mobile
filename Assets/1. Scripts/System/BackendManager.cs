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
            Debug.Log("초기화 성공 : " + bro);
        }
        else
        {
            Debug.LogError("초기화 실패 : " + bro);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
