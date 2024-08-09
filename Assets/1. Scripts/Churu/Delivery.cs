using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField] private Transform deliveryTransform;

    private Stack<GameObject> deliveryStack = new Stack<GameObject>();
    public Stack<GameObject> DeliveryStack
    {
        get { return deliveryStack; }
        set { deliveryStack = value; }
    }

    private void OnTriggerStay(Collider other)  
    {
        if (other.CompareTag("Box"))
        {
            other.gameObject.GetComponent<Rigidbody>();
            Utility.ObjectDrop(deliveryTransform, other.gameObject, null, deliveryStack, 2);
        }
    }
}
