using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeObject : MonoBehaviour
{
    [SerializeField] private GameObject objectB; // 변환될 B 오브젝트의 프리팹
    [SerializeField] private Transform newTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient") || other.CompareTag("Churu"))
        {
            GameObject newObject = Instantiate(objectB);
            newObject.transform.position = newTransform.position; 
            newObject.GetComponent<Rigidbody>().velocity = other.GetComponent<Rigidbody>().velocity;
            Destroy(other.gameObject);
        }
    }
}
