using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeObject : MonoBehaviour
{
    [SerializeField] private GameObject objectB; // ��ȯ�� B ������Ʈ�� ������

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
