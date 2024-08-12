using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private Vector3 direction = Vector3.forward;
    [SerializeField] private Transform ingredientStorage;
    [SerializeField] private Transform onBelt;

    private float timer = 2f;
    private float time = 0;

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
            yield return new WaitForSeconds(0.5f);

            if (cbStack.Count > 0)
            {
                PushStack();
                GameObject newChuru = cbStack.Pop();
                newChuru.transform.position = onBelt.position;
                newChuru.transform.SetParent(onBelt);

                if(!newChuru.GetComponent<Rigidbody>())
                    newChuru.AddComponent<Rigidbody>();
            }
        }
    }
    // �ӽ÷� ���� ���� ���� �߻� ���� �ذ� �ڵ�.
    // �����̾� ��Ʈ �ű� ������ ���� �ʱ�ȭ �� �ڽ� ������Ʈ���� �ٽ� Ǫ���ϴ� �ڵ�� ����, ���� �޸� ������ �ٸ� ���� �߻� �� �� ������ ����.
    // ���� ���� ��� ����� �ٽ� ���� ����.
    private void PushStack()
    {
        cbStack.Clear();
        foreach (Transform item in ingredientStorage)
        {
            cbStack.Push(item.gameObject);
        }
    }
}
