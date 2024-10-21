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

    //게임 종료 시 저장 코드
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
            targetUsage[stackable] = false; // 모든 타겟의 사용 상태를 초기화
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

            // 게임 실행 시 저장되어 있던 종업원 정보를 불러오는 부분
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
            // 불러오기 끝나고 리스트 삭제
            foreach (var item in employeesToRemove)
            {
                employee.Remove(item);
            }
        }
    }

    // 스택 저장하기 위해 인터페이스를 활용하여 스택 저장값 등록
    public void AddStackable(IStackable stackable)
    {
        if (!stackCount.Contains(stackable))
        {
            stackCount.Add(stackable);
        }
    }
    
    //종업원이 찾을 타겟 인터페이스를 활용하여 타겟 등록
    public void AddTarget(IStackable stackable)
    {
        if (!targetUsage.ContainsKey(stackable))
        {
            targetUsage[stackable] = false; // 기본값으로 false 설정
        }
    }

    // 종업원이 쓰고 있는 타겟 활용을 겹치지 않게 하기 위해 Bool값을 통해 조정
    public bool IsTargetBeingUsed(IStackable stackable)
    {
        if (targetUsage.ContainsKey(stackable))
        {
            return targetUsage[stackable]; // 딕셔너리에서 타겟의 사용 상태 반환
        }
        return false; // 타겟이 딕셔너리에 없으면 사용 중이 아님
    }

    // 종업원이 타겟을 쓰고 있는 경우 딕셔너리 Bool 값 변경을 통해 쓰고 있는지 확인
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

    // 종업원이 타겟을 다른 종업원에게 뺏기면 다른 타겟을 찾기
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

    // 네비매쉬 빌드
    public void NowNavMeshBake()
    {
        nms.BuildNavMesh();
    }

    // 컨베이어 벨트를 순차적으로 방문하기 위해 만든 함수
    public Transform ConveyorTransform(Employee employee)
    {
        employee.CbTransNum++;

        if (employee.CbTransNum == cbTrans.Count)
            employee.CbTransNum = 0;

        return cbTrans[employee.CbTransNum];
    }

    
}
