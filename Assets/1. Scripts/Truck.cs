using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CarType
{
    Go,
    Come
}

public class Truck : MonoBehaviour
{
    [SerializeField] private Transform[] checkPoint;

    [SerializeField] private GameObject workPoint;

    private NavMeshAgent na;
    [SerializeField] private CarType ct = CarType.Go;

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
            workPoint.SetActive(false);
            ct = CarType.Come;

            // boxStack이 모두 채워졌을 때 골드획득
            UIManager.Instance.AddGold(boxStack.Count * GameManager.Instance.P.GoldPerBox);
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
            // 다음 체크포인트가 있으면 이동
            if (currentCheckPoint < checkPoint.Length - 1)
            {
                currentCheckPoint++;
                na.SetDestination(checkPoint[currentCheckPoint].position);
            }
            else
            {
                // 모든 체크포인트를 지나면 이동 종료
                transform.rotation = Quaternion.Euler(0, -90, 0);
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
                // 다시 갔다가 돌아온다.
                ct = CarType.Go;
            }
        }
    }

    private void ClearBoxStack()
    {
        // 게임 오브젝트도 함께 없애기 위해 클리어 전에 리턴 풀링 해주는 코드.
        foreach (GameObject item in boxStack)
        {
            PoolingManager.Instance.ReturnObjecte(item);
        }
        boxStack.Clear();
        Debug.Log("boxStack Clear !");
    }
}
