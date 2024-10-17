using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

// �̰� �� ��ǰ
// �̹��� ť�� �۾��Ѱ� �ƴ϶� ��ųʸ��� Ȱ���� ����
// ��ųʸ��� ���� �� ������� ���� ���� Ǯ�� �۾��� �� ����� ��ųʸ��� �۾��ϼ�
public class PoolingManager : Singleton<PoolingManager>
{
    private Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();

    // �̰� �������� �Լ� �̸� ������ ������Ʈ ����� �κ��̴� �ϰ� ���� ����
    private GameObject CreateObj(GameObject ObjPrefab)
    {
        if(ObjPrefab == null)
        {
            Debug.LogError("���� ������Ʈ�� �����ϴ�.");
            return null;
        }
        GameObject newObj = Instantiate(ObjPrefab, transform);
        newObj.transform.localRotation = Quaternion.Euler(Vector3.zero);
        newObj.transform.localScale = Vector3.one;

        // ���⼭ �� �Ȱ��� �̸����� ����� ��ųʸ��� ������ �Ȼ���� ���� �ٶ�.
        // �� ������ ������Ʈ �̸�(clone) �ϰ� �ش� ť�� �����ϴ� ������
        // ���⼭ �̸� �������ִ� �κ��� �����ϱ� Ȯ�� �ʿ���
        newObj.gameObject.name = ObjPrefab.name;
        newObj.SetActive(false);
        return newObj;
    }

    // ���� �������� ������Ʈ �������� �Լ���
    public GameObject GetObj(GameObject prefab)
    {
        if(prefab == null)
        {
            Debug.LogError("������ ������Ʈ�� �����ϴ�.");
            return null;
        }
        if(!poolDictionary.ContainsKey(prefab.name))
        {
            // Ű�� ������ ť�� �������ִµ� ������ �̰� �³� ����
            // �׷��� �ش� Ű�� ������ �ؿ��� ���鵵�� �ص״µ�
            // ���� ���⼭ ���� poolDictionary.Add�� ������ִ°� ������ �ƴұ� ����
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

    // �̰� ������Ʈ �ٽ� �������� �� �ҷ����� �Լ���
    public void ReturnObjecte(GameObject returnPrefab)
    {
        if(returnPrefab == null)
        {
            Debug.LogError("��ȯ�� ������Ʈ�� �����ϴ�.");
            return;
        }

        returnPrefab.SetActive(false);

        // ���� if ������ �ش� Ű�� �ִ��� Ȯ���� ��
        if(poolDictionary.ContainsKey(returnPrefab.name))
        {
            if(returnPrefab.GetComponent<Rigidbody>())
                Destroy(returnPrefab.GetComponent<Rigidbody>());

            returnPrefab.transform.SetParent(transform);
            returnPrefab.transform.localScale = Vector3.one;
            poolDictionary[returnPrefab.name].Enqueue(returnPrefab);
        }
        else
        {
            // ������ ���� ���鼭 �׳� ���� ó�� �ع����µ�
            // ������ ������ ó���ϰ� ������ ���� �� ���ư��µ� ���� �����ϰ� �ٲ� �ʿ���� ������
            Debug.LogError($"{returnPrefab.name} �� �ش��ϴ� ť�� �����ϴ�.");

            // ���� �� �ٲ� �ڽ� �ִ� �ϸ� �� �κ��� ���ʿ��� ó���غ��°͵� ������ ����
            poolDictionary.Add($"{returnPrefab.name}", new Queue<GameObject>());
            
            // �ش� �κ��� Ű�� ��� Add �� ���� �ٽ� ������µ� ���� ���� ó���ؾ��ϳ� ������ 
            // ���� ó���Ұ� ���� ������ ���Ƽ� �׳� ���ֺ���
            Destroy(returnPrefab);
        }
    }
}
