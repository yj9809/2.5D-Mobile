using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using Sirenix.OdinInspector;

public enum CarType
{
    Go,
    Come
}

public class Truck : MonoBehaviour
{
    [SerializeField] private Transform[] checkPoint;

    [SerializeField] private GameObject workPoint;
    [SerializeField] private TMP_Text boxCountTxt;
    [SerializeField] private Animator Door;

    private NavMeshAgent na;
    private CarType ct = CarType.Go;
    private bool doorOpen = false;

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
            boxCountTxt.gameObject.SetActive(false);
            Door.SetBool("Open", true);
            ct = CarType.Come;

            // boxStack�� ��� ä������ �� ���ȹ��
            UIManager.Instance.AddGold(boxStack.Count * (int)GameManager.Instance.P.GoldPerBox);
            ClearBoxStack();
        }

        if (ct == CarType.Go)
            CheckPointMove();
        else
            ReturnCheckPointMove();
            

    }
    private void CheckPointMove()
    {
        //Door.SetBool("Open", true);
        if (!na.pathPending && na.remainingDistance <= na.stoppingDistance)
        {
            // ���� üũ����Ʈ�� ������ �̵�
            if (currentCheckPoint < checkPoint.Length - 1 && currentCheckPoint != 2)
            {
                currentCheckPoint++;
                na.SetDestination(checkPoint[currentCheckPoint].position);
            }
            else if(currentCheckPoint == 2)
            {
                if(!doorOpen)
                {
                    na.isStopped = true;
                    Door.SetBool("Open", true);
                }
                else
                {
                    na.isStopped = false;
                    currentCheckPoint++;
                    na.SetDestination(checkPoint[currentCheckPoint].position);
                }
            }
            else
            {
                // ��� üũ����Ʈ�� ������ �̵� ����
                Door.SetBool("Open", false);
                doorOpen = false;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                na.isStopped = true;
                workPoint.SetActive(true);
                boxCountTxt.gameObject.SetActive(true);
            }
        }
    }
    private void ReturnCheckPointMove()
    {
       if(doorOpen)
        {
            if (na.isStopped)
                na.isStopped = false;

            if (!na.pathPending && na.remainingDistance <= na.stoppingDistance)
            {
                // ���� üũ����Ʈ�� ������ �̵�
                if (currentCheckPoint > 0)
                {
                    Door.SetBool("Open", false);
                    currentCheckPoint--;
                    na.SetDestination(checkPoint[currentCheckPoint].position);
                }
                else
                {
                    // �ٽ� ���ٰ� ���ƿ´�.
                    doorOpen = false;
                    boxCountTxt.text = "0 / 5";
                    ct = CarType.Go;
                }
            }
        }
    }
    public void BoxCountTextUpdate()
    {
        boxCountTxt.text = $"{boxStack.Count} / 5";
    }
    private void ClearBoxStack()
    {
        // ���� ������Ʈ�� �Բ� ���ֱ� ���� Ŭ���� ���� ���� Ǯ�� ���ִ� �ڵ�.
        foreach (GameObject item in boxStack)
        {
            PoolingManager.Instance.ReturnObjecte(item);
        }
        boxStack.Clear();
        Debug.Log("boxStack Clear !");
    }
    public void DoorOpen()
    {
        doorOpen = true;
    }
}
