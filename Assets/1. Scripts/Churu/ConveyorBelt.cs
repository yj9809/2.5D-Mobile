using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private Vector3 direction = Vector3.forward;
    [SerializeField] private Transform ingredientStorage;
    [SerializeField] private Transform onBelt;

    public Transform IngredientStorage
    {
        get { return ingredientStorage; }
    }

    private Stack<GameObject> cbStack = new Stack<GameObject>();
    public Stack<GameObject> CbStack
    {
        get { return cbStack; }
        set { cbStack = value; }
    }

    private void OnCollisionStay(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = speed * direction;
        }
    }

    private void Start()
    {
        StartCoroutine(PlaceObject());
    }

    private IEnumerator PlaceObject()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);

            if (cbStack.Count > 0)
            {
                GameObject gameObject = cbStack.Pop();
                gameObject.transform.position = onBelt.position;
                gameObject.AddComponent<Rigidbody>();
            }
        }
    }
}
