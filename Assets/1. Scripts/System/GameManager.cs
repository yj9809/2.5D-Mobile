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
            targetUsage[stackable] = false; // 모든 타겟의 사용 상태를 초기화
        }
    }

    private void OnSceneLoaded(Scene previousScene, Scene newScene)
    {
        if(nms != null)
            nms.BuildNavMesh();
        else
        {
            nms = FindObjectOfType<NavMeshSurface>();
            nms.BuildNavMesh();
        }

        if (data.baseCost.step1)
        {
            Debug.Log("실행");
            StepSystem.Instance.Step1Obj[0].gameObject.SetActive(false);
            StepSystem.Instance.Step1Obj[1].gameObject.SetActive(true);
        }
    }

    public void AddStackable(IStackable stackable)
    {
        if (!stackCount.Contains(stackable))
        {
            stackCount.Add(stackable);
        }
    }
    public bool IsTargetBeingUsed(IStackable stackable)
    {
        if (targetUsage.ContainsKey(stackable))
        {
            return targetUsage[stackable]; // 딕셔너리에서 타겟의 사용 상태 반환
        }
        return false; // 타겟이 딕셔너리에 없으면 사용 중이 아님
    }
    public void SetTargetBeingUsed(IStackable stackable, bool isUsed)
    {
        if (targetUsage.ContainsKey(stackable))
        {
            targetUsage[stackable] = isUsed; // 딕셔너리에서 타겟의 사용 상태 업데이트
        }
    }

    public void UpdateTargets()
    {
        foreach (var employee in employees)
        {
            if (employee != null)
            {
                // 현재 목표가 있는 경우 새로운 목표로 업데이트
                employee.StartCoroutine(employee.CheckStack());
            }
        }
    }

    public void NowNavMeshBake()
    {
        nms.BuildNavMesh();
    }
}
