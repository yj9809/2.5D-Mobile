using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxStorage : MonoBehaviour, IStackable
{
    [SerializeField] private Transform[] boxTransform;


    private Stack<GameObject> boxStack = new Stack<GameObject>();

    public Stack<GameObject> BoxStack
    {
        get { return boxStack; }
        set { boxStack = value; }
    }

    private int boxTransformNum = 0;

    private void Start()
    {
        GameManager.Instance.stackCount.Add(this);
    }

    private void Update()
    {
        boxTransformNum = Mathf.Clamp(boxStack.Count / 10, 0, boxTransform.Length - 1);
    }
    private void OnCollisionStay(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Box") || collision.gameObject.CompareTag("Churu")) && boxStack.Count < 40)
        {
            Rigidbody rd = collision.transform.GetComponent<Rigidbody>();
            if(rd != null)
                Destroy(collision.transform.GetComponent<Rigidbody>());

            Utility.ObjectDrop(boxTransform[boxTransformNum], collision.gameObject, null, boxStack, 2);
        }
    }

    public int GetStackCount() => boxStack.Count;

    public Transform GetTransform() => transform.GetChild(0).transform;

    public int GetTypeNum() => 1;
}
