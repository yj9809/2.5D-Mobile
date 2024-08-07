using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ChuruManager : MonoBehaviour
{
    [FoldoutGroup("ChuruManager")] [SerializeField] protected GameObject objPrefab;
    [FoldoutGroup("ChuruManager")] [SerializeField] protected Transform objSpawnPoint;
    [FoldoutGroup("ChuruManager")] [SerializeField] protected float objSpawnTime = 2f;
    [FoldoutGroup("ChuruManager")] [SerializeField] protected int maxObj = 10;

    protected float spawnTimer = 0f;
    protected Stack<GameObject> churuStack = new Stack<GameObject>();
    public Stack<GameObject> ChuruStack
    {
        get { return churuStack; }
        set { churuStack = value; }
    }

    // Update is called once per frame
    void Update()
    {
        SpawnGameObject();
    }

    protected void SpawnGameObject()
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
            Debug.Log("Object is Full !");
        }
    }
}
