using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public interface IStackable
{
    int GetStackCount();
    Transform GetTransform();
    int GetTypeNum();
}

public class GameManager : Singleton<GameManager>
{
    private Player p;
    public Player P
    {
        get
        {
            if (p == null)
            {
                p = FindObjectOfType<Player>();
            }
            return p;
        }
    }

    private MainCamera mainCamera;
    public MainCamera MainCamera
    {
        get
        {
            if (mainCamera == null)
                mainCamera = FindObjectOfType<MainCamera>();
            return mainCamera;
        }
    }

    public List<GameObject> employee;

    private NavMeshSurface nms;
    private DataManager data;

    public List<IStackable> stackCount = new List<IStackable>();
    public List<Transform> cbTrans = new List<Transform>();
    private List<Employee> employees = new List<Employee>();

    private Dictionary<IStackable, bool> targetUsage = new Dictionary<IStackable, bool>();

    public string sceneName;

    //���� ���� �� ���� �ڵ�
    private void OnApplicationQuit()
    {
        DataManager.Instance.GameDataUpdate();
    }
    protected override void Awake()
    {
        base.Awake();
        data = DataManager.Instance;
        Application.targetFrameRate = 60;
        nms = FindObjectOfType<NavMeshSurface>();
    }

    private void Start()
    {
        SceneManager.activeSceneChanged += OnSceneLoaded;
        employees.AddRange(FindObjectsOfType<Employee>());
        foreach (var stackable in stackCount)
        {
            targetUsage[stackable] = false; // ��� Ÿ���� ��� ���¸� �ʱ�ȭ
        }
    }

    private void OnSceneLoaded(Scene previousScene, Scene newScene)
    {
        if(sceneName == "Game")
        {
            if (nms != null)
                nms.BuildNavMesh();
            else
            {
                nms = FindObjectOfType<NavMeshSurface>();
                nms.BuildNavMesh();
            }

            #if !UNITY_EDITOR
             if(!data.baseCost.gameProgressBool["NewGame"])
                        {
                            data.baseCost.gameProgressBool["NewGame"] = true;
                            Social.ReportProgress(GPGSIds.achievement, 100f, null);
                            data.GameDataUpdate();
                        }
            #endif

            List<GameObject> employeesToRemove = new List<GameObject>();
            int employeeNum = 0;

            // ���� ���� �� ����Ǿ� �ִ� ������ ������ �ҷ����� �κ�
            foreach (var item in employee)
            {
                if (data.baseCost.employeeList.Contains(item.name))
                {
                    Debug.Log(employeeNum);
                    GameObject newEmployee;
                    if (employeeNum != 4)
                    {
                        newEmployee = Instantiate(item.gameObject, new Vector3(employeeNum, 0, employeeNum), Quaternion.identity);
                    }
                    else
                    {
                        Vector3 pos = FindObjectOfType<BoxPackaging>().transform.GetChild(1).transform.position;
                        newEmployee = Instantiate(item.gameObject);
                        Destroy(newEmployee.GetComponent<NavMeshAgent>());
                        newEmployee.transform.position = pos;
                        newEmployee.GetComponent<Employee>().PackaingEmployee();
                    }

                    newEmployee.name = item.name;
                    P.employee.Add(newEmployee.GetComponent<Employee>());
                    employeesToRemove.Add(item);
                    employeeNum++;
                }
            }
            // �ҷ����� ������ ����Ʈ ����
            foreach (var item in employeesToRemove)
            {
                employee.Remove(item);
            }
        }
    }

    // ���� �����ϱ� ���� �������̽��� Ȱ���Ͽ� ���� ���尪 ���
    public void AddStackable(IStackable stackable)
    {
        if (!stackCount.Contains(stackable))
        {
            stackCount.Add(stackable);
        }
    }
    
    //�������� ã�� Ÿ�� �������̽��� Ȱ���Ͽ� Ÿ�� ���
    public void AddTarget(IStackable stackable)
    {
        if (!targetUsage.ContainsKey(stackable))
        {
            targetUsage[stackable] = false; // �⺻������ false ����
        }
    }

    // �������� ���� �ִ� Ÿ�� Ȱ���� ��ġ�� �ʰ� �ϱ� ���� Bool���� ���� ����
    public bool IsTargetBeingUsed(IStackable stackable)
    {
        if (targetUsage.ContainsKey(stackable))
        {
            return targetUsage[stackable]; // ��ųʸ����� Ÿ���� ��� ���� ��ȯ
        }
        return false; // Ÿ���� ��ųʸ��� ������ ��� ���� �ƴ�
    }

    // �������� Ÿ���� ���� �ִ� ��� ��ųʸ� Bool �� ������ ���� ���� �ִ��� Ȯ��
    public void SetTargetBeingUsed(IStackable stackable, bool isUsed)
    {
        if (stackable == null)
        {
            Debug.LogError("stackable is null");
            return;
        }

        if (targetUsage.ContainsKey(stackable))
        {
            targetUsage[stackable] = isUsed;
        }
        else
        {
            Debug.LogWarning("The specified stackable is not in the dictionary.");
        }
    }

    // �������� Ÿ���� �ٸ� ���������� ����� �ٸ� Ÿ���� ã��
    public void UpdateTargets()
    {
        foreach (var employee in employees)
        {
            if (employee != null)
            {
                // ���� ��ǥ�� �ִ� ��� ���ο� ��ǥ�� ������Ʈ
                employee.StartCoroutine(employee.CheckStack());
            }
        }
    }

    // �׺�Ž� ����
    public void NowNavMeshBake()
    {
        nms.BuildNavMesh();
    }

    // �����̾� ��Ʈ�� ���������� �湮�ϱ� ���� ���� �Լ�
    public Transform ConveyorTransform(Employee employee)
    {
        employee.CbTransNum++;

        if (employee.CbTransNum == cbTrans.Count)
            employee.CbTransNum = 0;

        return cbTrans[employee.CbTransNum];
    }

    
}
