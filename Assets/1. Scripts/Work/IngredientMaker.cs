using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class IngredientMaker : MonoBehaviour, IStackable
{
    [TabGroup("IngredientMaker")] [SerializeField] private GameObject objPrefab;
    [TabGroup("IngredientMaker")] [SerializeField] private Transform objSpawnPoint;
    [TabGroup("IngredientMaker")] [SerializeField] private float objSpawnTime = 2f;
    [TabGroup("IngredientMaker")] [SerializeField] private int maxObj = 10;

    private float spawnTimer = 0f;

    public float ObjSpawnTime
    {
        get { return objSpawnTime; }
        set { objSpawnTime = value; }
    }

    private Stack<GameObject> churuStack = new Stack<GameObject>();
    public Stack<GameObject> ChuruStack
    {
        get { return churuStack; }
        set { churuStack = value; }
    }

    private GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;
        gm.AddStackable(this); // 리스트에 추가
    }

    private void Update()
    {
        SpawnGameObject();
//#if !UNITY_EDITOR
//        UIManager.Instance.SetIngredientMaker(this);
//#endif

        // 타겟 업데이트 로직
        if (ChuruStack.Count == 0)
        {
            gm.UpdateTargets();
        }
    }

    private void SpawnGameObject()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= objSpawnTime)
        {
            if (ChuruStack.Count < maxObj)
            {
                Utility.ObjectDrop(objSpawnPoint, objPrefab, null, ChuruStack, 0);
            }
            spawnTimer = 0f;
        }
    }

    public int GetStackCount() => churuStack.Count;

    public Transform GetTransform() => transform.GetChild(0).transform;
    public int GetTypeNum() => 0;
}
