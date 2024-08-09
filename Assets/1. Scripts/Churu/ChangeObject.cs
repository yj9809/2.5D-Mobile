using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeObject : MonoBehaviour
{
    [SerializeField] private GameObject objectB; // 변환될 B 오브젝트의 프리팹
    [SerializeField] private Transform newTransform;

    private PoolingManager pool;

    private void Start()
    {
        pool = PoolingManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient") || other.CompareTag("Churu"))
        {
            GameObject newObject = pool.GetObj(objectB);
            newObject.name = objectB.name;
            newObject.transform.position = newTransform.position; 

            if(!newObject.GetComponent<Rigidbody>())
            {
                newObject.AddComponent<Rigidbody>();
                newObject.GetComponent<Rigidbody>().freezeRotation = true;
            }
            newObject.GetComponent<Rigidbody>().velocity = other.GetComponent<Rigidbody>().velocity;
            pool.ReturnObjecte(other.gameObject);
        }
    }
}
