using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PoolingManager : Singleton<PoolingManager>
{
    private Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();

    private GameObject CreateObj(GameObject ObjPrefab)
    {
        if(ObjPrefab == null)
        {
            Debug.LogError("만들 오브젝트가 없습니다.");
            return null;
        }
        GameObject newObj = Instantiate(ObjPrefab, transform);
        newObj.SetActive(false);
        return newObj;
    }

    public GameObject GetObj(GameObject prefab)
    {
        if(prefab == null)
        {
            Debug.LogError("가져올 오브젝트가 없습니다.");
            return null;
        }
        if(!poolDictionary.ContainsKey(prefab.name))
        {
            poolDictionary[prefab.name] = new Queue<GameObject>();
        }

        Queue<GameObject> objPool = poolDictionary[prefab.name];

        if(objPool.Count <= 0)
        {
            GameObject newObj = CreateObj(prefab);
            objPool.Enqueue(newObj);
        }

        GameObject objToReturn = objPool.Dequeue();
        objToReturn.SetActive(true);
        return objToReturn;
    }

    public void ReturnObjecte(GameObject returnPrefab)
    {
        if(returnPrefab == null)
        {
            Debug.LogError("반환할 오브젝트가 없습니다.");
            return;
        }

        returnPrefab.SetActive(false);

        if(poolDictionary.ContainsKey(returnPrefab.name))
        {
            if(returnPrefab.GetComponent<Rigidbody>())
                Destroy(returnPrefab.GetComponent<Rigidbody>());

            returnPrefab.transform.SetParent(transform);
            poolDictionary[returnPrefab.name].Enqueue(returnPrefab);
        }
        else
        {
            Debug.LogError($"{returnPrefab.name} 에 해당하는 큐가 없습니다.");
            Destroy(returnPrefab);
        }
    }
}
