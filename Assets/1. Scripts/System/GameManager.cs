using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Android;

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

    protected override void Awake()
    {
        base.Awake();
        data = DataManager.Instance;
        Application.targetFrameRate = 60; // 60프레임 고정
    }

    private void Start()
    {
        SceneManager.activeSceneChanged += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene previousScene, Scene newScene)
    {
        if(data.baseCost.step1)
        {
            Debug.Log("실행");
            StepSystem.Instance.Step1Obj[0].gameObject.SetActive(false);
            StepSystem.Instance.Step1Obj[1].gameObject.SetActive(true);
        }
    }
}
