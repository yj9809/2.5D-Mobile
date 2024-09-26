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
        //nms.BuildNavMesh();
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

            List<GameObject> employeesToRemove = new List<GameObject>();
            int employeeNum = 0;

            foreach (var item in employee)
            {
                if (data.baseCost.employeeList.Contains(item.name))
                {
                    GameObject newEmployee;
                    if (employeeNum != 3)
                    {
                        newEmployee = Instantiate(item.gameObject, Vector3.zero, Quaternion.identity);
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
                    employeesToRemove.Add(item);
                    p.employee.Add(newEmployee.GetComponent<Employee>());
                    employeeNum++;
                }
            }

            foreach (var item in employeesToRemove)
            {
                employee.Remove(item);
            }
        }

        // ������Ʈ �ܰ踦 �����ϱ� ���� �ӽ÷� �־��
        //if (data.baseCost.step1)
        //{
        //    Debug.Log("����");
        //    StepSystem.Instance.Step1Obj[0].gameObject.SetActive(false);
        //    StepSystem.Instance.Step1Obj[1].gameObject.SetActive(true);
        //}
    }

    public void AddStackable(IStackable stackable)
    {
        if (!stackCount.Contains(stackable))
        {
            stackCount.Add(stackable);
        }
    }
    public void AddTarget(IStackable stackable)
    {
        if (!targetUsage.ContainsKey(stackable))
        {
            targetUsage[stackable] = false; // �⺻������ false ����
        }
    }
    public bool IsTargetBeingUsed(IStackable stackable)
    {
        if (targetUsage.ContainsKey(stackable))
        {
            return targetUsage[stackable]; // ��ųʸ����� Ÿ���� ��� ���� ��ȯ
        }
        return false; // Ÿ���� ��ųʸ��� ������ ��� ���� �ƴ�
    }
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

    public void NowNavMeshBake()
    {
        nms.BuildNavMesh();
    }
}
