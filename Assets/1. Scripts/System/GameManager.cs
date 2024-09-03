using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IStackable
{
    int GetStackCount();
    Transform GetTransform();
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

    private DataManager data;

    public List<IStackable> stackCount = new List<IStackable>();
    private List<Employee> employees = new List<Employee>();

    protected override void Awake()
    {
        base.Awake();
        data = DataManager.Instance;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        SceneManager.activeSceneChanged += OnSceneLoaded;
        employees.AddRange(FindObjectsOfType<Employee>());
    }

    private void OnSceneLoaded(Scene previousScene, Scene newScene)
    {
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

    public void RemoveStackable(IStackable stackable)
    {
        if (stackCount.Contains(stackable))
        {
            stackCount.Remove(stackable);
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
}
