using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CarType
{
    Go,
    Come
}

public class TestCar : MonoBehaviour
{
    [SerializeField] private Transform[] checkPoint;

    [SerializeField] private GameObject workPoint;

    private NavMeshAgent na;
    private CarType ct = CarType.Go;

    [SerializeField] private Stack<GameObject> boxStack = new Stack<GameObject>();
    public Stack<GameObject> BoxStack
    {
        get { return boxStack; }
        set { boxStack = value;}
    }

    private int currentCheckPoint = 1;

    private int goldPerBox = 10;
    // Start is called before the first frame update
    void Start()
    {
        na = GetComponent<NavMeshAgent>();
        na.SetDestination(checkPoint[currentCheckPoint].position);
        workPoint.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(boxStack.Count >= 5)
        {
            workPoint.SetActive(false);
            ct = CarType.Come;

            // boxStack�� ��� ä������ �� ���ȹ��
            UIManager.Instance.AddGold(boxStack.Count * goldPerBox);
            ClearBoxStack();
        }

        if (ct == CarType.Go)
            CheckPointMove();
        else
            ReturnCheckPointMove();

    }
    private void CheckPointMove()
    {
        if (!na.pathPending && na.remainingDistance <= na.stoppingDistance)
        {
            // ���� üũ����Ʈ�� ������ �̵�
            if (currentCheckPoint < checkPoint.Length - 1)
            {
                currentCheckPoint++;
                na.SetDestination(checkPoint[currentCheckPoint].position);
            }
            else
            {
                // ��� üũ����Ʈ�� ������ �̵� ����
                na.isStopped = true;
                workPoint.SetActive(true);
            }
        }
    }
    private void ReturnCheckPointMove()
    {
        if (na.isStopped)
            na.isStopped = false;

        if (!na.pathPending && na.remainingDistance <= na.stoppingDistance)
        {
            // ���� üũ����Ʈ�� ������ �̵�
            if (currentCheckPoint > 0)
            {
                currentCheckPoint--;
                na.SetDestination(checkPoint[currentCheckPoint].position);
            }
            else
            {
                // ��� üũ����Ʈ�� ������ �̵� ����
                ct = CarType.Go;
            }
        }
    }

    private void ClearBoxStack()
    {
        foreach (GameObject item in boxStack)
        {
            PoolingManager.Instance.ReturnObjecte(item);
        }
        boxStack.Clear();
        Debug.Log("boxStack Clear !");
    }
}
