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
    // 임시로 스택 관련 버그 발생 문제 해결 코드.
    // 컨베이어 벨트 옮길 때마다 스택 초기화 후 자식 오브젝트들을 다시 푸쉬하는 코드로 변경, 추후 메모리 문제나 다른 문제 발생 할 수 있을꺼 같음.
    // 추후 좋은 방법 생기면 다시 수정 예정.
    private void PushStack()
    {
        cbStack.Clear();
        foreach (Transform item in ingredientStorage)
        {
            cbStack.Push(item.gameObject);
        }
    }
}
