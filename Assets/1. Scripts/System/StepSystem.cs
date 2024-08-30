using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSystem : Singleton<StepSystem>
{
    [SerializeField] private GameObject[] step1Obj;
    public GameObject[] Step1Obj
    {
        get { return step1Obj; }
    }
}
