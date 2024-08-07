using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxStorage : MonoBehaviour
{
    [SerializeField] private Transform boxTransform;

    public Transform BoxTrans
    {
        get { return boxTransform; }
    }

    private Stack<GameObject> boxStack = new Stack<GameObject>();
    public Stack<GameObject> BoxStack
    {
        get { return boxStack; }
        set { boxStack = value; }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            Destroy(other.GetComponent<Rigidbody>());
            Utility.ObjectDrop(transform, other.gameObject, null, boxStack, 2);
        }
    }
}
