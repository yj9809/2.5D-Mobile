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
        gm.stackCount.Add(this);
    }
    // Update is called once per frame
    void Update()
    {
        SpawnGameObject();
        
        //테스트용
        //UIManager.Instance.SetIngredientMaker(this);
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
        else if(ChuruStack.Count >= maxObj)
        {
            //Debug.Log("Object is Full !");
        }
    }

    public int GetStackCount() => churuStack.Count;

    public Transform GetTransform() => transform.GetChild(0).transform;
    public int GetTypeNum() => 0;
}
