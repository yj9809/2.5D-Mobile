using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxStorage : MonoBehaviour
{
    [SerializeField] private Transform[] boxTransform;


    private Stack<GameObject> boxStack = new Stack<GameObject>();

    public Stack<GameObject> BoxStack
    {
        get { return boxStack; }
        set { boxStack = value; }
    }

    private int boxTransformNum = 0;
    
    private void Update()
    {
        boxTransformNum = Mathf.Clamp(boxStack.Count / 10, 0, boxTransform.Length - 1);
    }
    private void OnTriggerStay(Collider other)
    {
        if ((other.CompareTag("Box") || other.CompareTag("Churu")) && boxStack.Count < 40)
        {
            Destroy(other.GetComponent<Rigidbody>());
            Utility.ObjectDrop(boxTransform[boxTransformNum], other.gameObject, null, boxStack, 2);
        }
    }
}
