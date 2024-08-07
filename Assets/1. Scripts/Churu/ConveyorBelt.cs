using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private Vector3 direction = Vector3.forward;
    [SerializeField] private Collider test;
    [SerializeField] private Transform testTransform;
    public Transform TestTransform
    {
        get { return testTransform; }
    }

    private Stack<GameObject> cbStack = new Stack<GameObject>();
    public Stack<GameObject> CbStack
    {
        get { return cbStack; }
        set { cbStack = value; }
    }
    void OnCollisionStay(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = speed * direction;
        }
    }
}
