using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestCar : MonoBehaviour
{
    [SerializeField] private Transform[] checkPoint;

    [SerializeField] private GameObject workPoint;

    private Stack<GameObject> testStack = new Stack<GameObject>();
    public Stack<GameObject> TestStack
    {
        get { return testStack; }
        set
        {
            testStack = value;
        }
    }
    private NavMeshAgent na;

    private int currentCheckPoint = 0;
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
}
