using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Android;


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
            if(p == null)
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

    protected override void Awake()
    {
        base.Awake();
        data = DataManager.Instance;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        SceneManager.activeSceneChanged += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene previousScene, Scene newScene)
    {
        if(data.baseCost.step1)
        {
            Debug.Log("½ÇÇà");
            StepSystem.Instance.Step1Obj[0].gameObject.SetActive(false);
            StepSystem.Instance.Step1Obj[1].gameObject.SetActive(true);
        }
    }
}
