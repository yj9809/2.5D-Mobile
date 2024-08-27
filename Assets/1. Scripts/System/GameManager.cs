using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
}
