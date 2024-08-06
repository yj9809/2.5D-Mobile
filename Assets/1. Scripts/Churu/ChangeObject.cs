using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeObject : MonoBehaviour
{
    [SerializeField] private GameObject objectB; // 변환될 B 오브젝트의 프리팹

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient"))
        {
            Transform oldTransform = other.transform;
            GameObject newObject = Instantiate(objectB, oldTransform.position, oldTransform.rotation);
            newObject.GetComponent<Rigidbody>().velocity = other.GetComponent<Rigidbody>().velocity;
            Destroy(other.gameObject);
        }
    }
}
