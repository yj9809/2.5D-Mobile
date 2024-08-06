using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Branch Test !");
        TestFunction();
    }
    private void TestFunction()
    {
        Debug.Log("function Test");
    }
}
