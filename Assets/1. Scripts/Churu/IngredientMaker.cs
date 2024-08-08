using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class IngredientMaker : MonoBehaviour
{
    [TabGroup("IngredientMaker")] [SerializeField] private GameObject objPrefab;
    [TabGroup("IngredientMaker")] [SerializeField] private Transform objSpawnPoint;
    [TabGroup("IngredientMaker")] [SerializeField] private float objSpawnTime = 2f;
    [TabGroup("IngredientMaker")] [SerializeField] private int maxObj = 10;

    private float spawnTimer = 0f;
    private Stack<GameObject> churuStack = new Stack<GameObject>();
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
}
