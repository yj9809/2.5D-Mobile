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
            // 이쯤에다 코인 올라가는거 추가하면 될 듯
            workPoint.SetActive(false);
            ct = CarType.Come;
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
            // 다음 체크포인트가 있으면 이동
            if (currentCheckPoint < checkPoint.Length - 1)
            {
                currentCheckPoint++;
                na.SetDestination(checkPoint[currentCheckPoint].position);
            }
            else
            {
                // 모든 체크포인트를 지나면 이동 종료
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
            // 다음 체크포인트가 있으면 이동
            if (currentCheckPoint > 0)
            {
                currentCheckPoint--;
                na.SetDestination(checkPoint[currentCheckPoint].position);
            }
            else
            {
                // 모든 체크포인트를 지나면 이동 종료
                na.isStopped = true;
            }
        }
    }
}
