using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public interface IObjectDataSave
{
    void ObjectDataSave();
}

public class BaseCost
{
    public int baseSpeedUpgradeCost = 100;
    public int baseMaxObjStackCountUpgradeCost = 100;
    public int baseGoldPerBoxUpgradeCost = 100;
    public int baseSpeedUpgradeCount = 0;
    public int baseMaxObjStackCountUpgradeCount = 0;
    public int baseGoldPerBoxUpgradeCount = 0;
    public int baseEmployeeSpeedUpgradeCost = 100;
    public int baseEmployeeMaxObjStackCountUpgradeCost = 100;
    public int baseEmployeeAddCost = 100;
    public int baseEmployeeSpeedUpgradeCount = 0;
    public int baseEmployeeMaxObjStackCountUpgradeCount = 0;
    public int baseEmployeeAddCount = 0;
    public int baseUpgradeMaxCount = 5;

    // Player ������
    public float playerBaseSpeed = 5;
    public float playerBaseCartSpeed = 2.5f;
    public int playerMaxObjStackCount = 3;
    public int playerGold = 100;
    public int playerGoldPerBox = 50;
    public List<string> employeeList = new List<string>();

    // Employee ������
    public float employeeBaseSpeed = 3;
    public float employeeBaseCartSpeed = 1.5f;
    public int employeeBaseMaxObjStackCount = 3;

    // ������Ʈ ������
    public int conveyorBeltBoxStorageStackCount = 0;
    public int churuStorageStackCount = 0;
    public int packagingWaitObjCount = 0;
    public int packagingBoxStorageStackCount = 0;

    public int guideStep = 0;
    public bool unlockOffice = false;
    public bool unlockContainer_1 = false;
    public bool unlockMachine_1 = false;
    public bool unlockContainer_2 = false;
    public bool unlockMachine_2 = false;
    public bool unlockStore = false;
}

public class DataManager : Singleton<DataManager>
{
    public BaseCost baseCost = new BaseCost();

    private List<IObjectDataSave> objectDataList = new List<IObjectDataSave>();

    public string path;
    public string fileName = "SaveFile";
    public string filePath;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        path = Application.persistentDataPath + "/Save";
        Debug.Log("���� ���" + path);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        filePath = Path.Combine(path, fileName);

    }

    public void SaveData()
    {
        ObjStackCountSave();

        string data = JsonUtility.ToJson(baseCost);
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
        string encode = System.Convert.ToBase64String(bytes);
        File.WriteAllText(filePath, encode);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(filePath);
        byte[] bytes = System.Convert.FromBase64String(data);
        string decode = System.Text.Encoding.UTF8.GetString(bytes);
        baseCost = JsonUtility.FromJson<BaseCost>(decode);
    }

    public void AddObjStackCountList(IObjectDataSave iStackCountSave)
    {
        objectDataList.Add(iStackCountSave);
    }

    private void ObjStackCountSave()
    {
        foreach (var item in objectDataList)
        {
            item.ObjectDataSave();
        }
    }

    public void DataClear()
    {
        if (CheckFile())
            File.Delete(filePath);
    }

    public bool CheckFile()
    {
        return File.Exists(filePath);
    }
}


