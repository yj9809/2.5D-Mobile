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
    protected Stack<GameObject> churu = new Stack<GameObject>();
    public Stack<GameObject> Churu
    {
        get { return churu; }
        set { churu = value; }
    }

    protected virtual void Init()
    {

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
            if (churu.Count < maxObj)
            {
                GameObject gameObject = Instantiate(objPrefab);
                Vector3 pos = new Vector3(objSpawnPoint.position.x
                    , objSpawnPoint.position.y + Utility.ObjRendererCheck(gameObject) * churu.Count
                    , objSpawnPoint.position.z);
                gameObject.transform.position = pos;
                churu.Push(gameObject);
                gameObject.transform.SetParent(objSpawnPoint);
            }
            spawnTimer = 0f;
        }
        else if(churu.Count >= maxObj)
        {
            Debug.Log("Object is Full !");
        }
    }
}
