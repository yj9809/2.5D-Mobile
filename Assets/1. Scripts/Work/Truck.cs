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

public class Truck : MonoBehaviour, IObjectDataSave
{
    [SerializeField] private Transform[] checkPoint;
    [SerializeField] private GameObject workPoint;
    [SerializeField] private TMP_Text boxCountTxt;
    [SerializeField] private Animator Door;

    private NavMeshAgent na;
    private DataManager data;
    private GameManager gm;

    private CarType ct = CarType.Go;

    private bool doorOpen = false;

    [SerializeField] private Stack<GameObject> boxStack = new Stack<GameObject>();
    public Stack<GameObject> BoxStack
    {
        get { return boxStack; }
        set { boxStack = value;}
    }

    [SerializeField] private Transform boxLoadingTransform;
    public Transform BoxLoadingTransform { get { return boxLoadingTransform; } }


    private int currentCheckPoint = 1;

    // Start is called before the first frame update
    void Start()
    {
        data = DataManager.Instance;
        na = GetComponent<NavMeshAgent>();
        gm = GameManager.Instance;
        na.SetDestination(checkPoint[currentCheckPoint].position);
        data.AddObjStackCountList(this);
        workPoint.SetActive(false);
        SetBoxStackCount();
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
            Debug.Log($"gold : {gm.P.GoldPerBox}, buff : {gm.P.buffGold}");
            UIManager.Instance.AddGold(boxStack.Count * (int)(gm.P.GoldPerBox + (gm.P.GoldPerBox * gm.P.buffGold)));
            ClearBoxStack();
        }

        if (ct == CarType.Go)
            CheckPointMove();
        else
            ReturnCheckPointMove();
            

    }
    private void SetBoxStackCount()
    {
        for (int i = 0; i < data.baseCost.objectData["truckBoxStackCount"]; i++)
        {
            boxStack.Push(null);
        }
        BoxCountTextUpdate();
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
                    BoxCountTextUpdate();
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
    }
    public void DoorOpen()
    {
        doorOpen = true;
    }

    public void ObjectDataSave()
    {
        if(boxStack.Count <= 4)
            data.baseCost.objectData["truckBoxStackCount"] = boxStack.Count;
    }
}
