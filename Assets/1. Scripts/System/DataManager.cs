using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using BackEnd;

public interface IObjectDataSave
{
    void ObjectDataSave();
}

public class BaseCost
{
    public Dictionary<string, int> upgradeCosts = new Dictionary<string, int>
    {
        { "baseSpeedUpgradeCost", 100 },
        { "baseMaxObjStackCountUpgradeCost", 100 },
        { "baseGoldPerBoxUpgradeCost", 100 },
        { "baseEmployeeSpeedUpgradeCost", 100 },
        { "baseEmployeeMaxObjStackCountUpgradeCost", 100 },
        { "baseEmployeeAddCost", 100 },
        { "baseUpgradeMaxCount", 5 },
        { "baseSpeedUpgradeCount", 0 },
        { "baseMaxObjStackCountUpgradeCount", 0 },
        { "baseGoldPerBoxUpgradeCount", 0 },
        { "baseEmployeeSpeedUpgradeCount", 0 },
        { "baseEmployeeMaxObjStackCountUpgradeCount", 0 },
        { "baseEmployeeAddCount", 0 }
    };

    //public int baseSpeedUpgradeCost = 100;
    //public int baseMaxObjStackCountUpgradeCost = 100;
    //public int baseGoldPerBoxUpgradeCost = 100;
    //public int baseSpeedUpgradeCount = 0;
    //public int baseMaxObjStackCountUpgradeCount = 0;
    //public int baseGoldPerBoxUpgradeCount = 0;
    //public int baseEmployeeSpeedUpgradeCost = 100;
    //public int baseEmployeeMaxObjStackCountUpgradeCost = 100;
    //public int baseEmployeeAddCost = 100;
    //public int baseEmployeeSpeedUpgradeCount = 0;
    //public int baseEmployeeMaxObjStackCountUpgradeCount = 0;
    //public int baseEmployeeAddCount = 0;
    //public int baseUpgradeMaxCount = 5;

    // Player ������
    public Dictionary<string, float> playerData = new Dictionary<string, float>
    {   
        { "baseSpeed", 5 },
        { "baseCartSpeed", 2.5f },
        { "maxObjStackCount", 3 },
        { "gold", 100},
        { "goldPerBox", 50 }
    };
    //public float playerBaseSpeed = 5;
    //public float playerBaseCartSpeed = 2.5f;
    //public int playerMaxObjStackCount = 3;
    //public int playerGold = 100;
    //public int playerGoldPerBox = 50;

    public List<string> employeeList = new List<string>();

    // Employee ������
    public Dictionary<string, float> employeeData = new Dictionary<string, float>
    {
        { "employeeSpeed", 3 },
        { "employeeCartSpeed", 1.5f },
        { "employeeMaxObjStackCount", 3 }
    };
    //public float employeeBaseSpeed = 3;
    //public float employeeBaseCartSpeed = 1.5f;
    //public int employeeBaseMaxObjStackCount = 3;

    // ������Ʈ ������
    public Dictionary<string, int> objectData = new Dictionary<string, int>
    {
        { "conveyorBeltBoxStorageStackCount", 0 },
        { "churuStorageStackCount", 0 },
        { "packagingWaitObjCount", 0 },
        { "packagingBoxStorageStackCount", 0 }
    };
    //public int conveyorBeltBoxStorageStackCount = 0;
    //public int churuStorageStackCount = 0;
    //public int packagingWaitObjCount = 0;
    //public int packagingBoxStorageStackCount = 0;

    public Dictionary<string, bool> gameProgressBool = new Dictionary<string, bool>
    {
        { "unlockOffice", false },
        { "unlockContainer_1", false },
        { "unlockMachine_1", false },
        { "unlockContainer_2", false },
        { "unlockMachine_2", false },
        { "unlockStore", false }
    };
    public int guideStep = 0;

    //public bool unlockOffice = false;
    //public bool unlockContainer_1 = false;
    //public bool unlockMachine_1 = false;
    //public bool unlockContainer_2 = false;
    //public bool unlockMachine_2 = false;
    //public bool unlockStore = false;
}

public class DataManager : Singleton<DataManager>
{
    public BaseCost baseCost;

    private List<IObjectDataSave> objectDataList = new List<IObjectDataSave>();

    public string path;
    public string fileName = "SaveFile";
    public string filePath;

    private string gameDataRowInDate = string.Empty;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        //path = Application.persistentDataPath + "/Save";
        //Debug.Log("���� ���" + path);
        //if (!Directory.Exists(path))
        //{
        //    Directory.CreateDirectory(path);
        //}

        //filePath = Path.Combine(path, fileName);

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
    public void GameDataInsert()
    {
        if (baseCost == null)
            baseCost = new BaseCost();

        Param param = new Param();
        param.Add("upgradeCosts", baseCost.upgradeCosts);
        param.Add("playerData", baseCost.playerData);
        param.Add("employeeList", baseCost.employeeList);
        param.Add("employeeData", baseCost.employeeData);
        param.Add("objectData", baseCost.objectData);
        param.Add("gameProgressBool", baseCost.gameProgressBool);
        param.Add("guideStep", baseCost.guideStep);

        Debug.Log("���� ���� ������ ������ ��û�մϴ�.");
        var bro = Backend.GameData.Insert("UserData", param);

        if (bro.IsSuccess())
        {
            Debug.Log("���� ���� ������ ���Կ� �����߽��ϴ�. : " + bro);

            //������ ���� ������ �������Դϴ�.  
            gameDataRowInDate = bro.GetInDate();
        }
        else
        {
            GameDataInsert();
            Debug.LogError("���� ���� ������ ���Կ� �����߽��ϴ�. : " + bro);
        }
    }

    public void GameDataGet()
    {
        // Step 3. ���� ���� �ҷ����� �����ϱ�
        Debug.Log("���� ���� ��ȸ �Լ��� ȣ���մϴ�.");

        var bro = Backend.GameData.GetMyData("UserData", new Where());

        if (bro.IsSuccess())
        {
            Debug.Log("���� ���� ��ȸ�� �����߽��ϴ�. : " + bro);


            LitJson.JsonData gameDataJson = bro.FlattenRows(); // Json���� ���ϵ� �����͸� �޾ƿɴϴ�.  

            // �޾ƿ� �������� ������ 0�̶�� �����Ͱ� �������� �ʴ� ���Դϴ�.  
            if (gameDataJson.Count <= 0)
            {
                Debug.LogWarning("�����Ͱ� �������� �ʽ��ϴ�.");
            }
            else
            {
                gameDataRowInDate = gameDataJson[0]["inDate"].ToString(); //�ҷ��� ���� ������ �������Դϴ�.  

                baseCost = new BaseCost();

                baseCost.guideStep = int.Parse(gameDataJson[0]["guideStep"].ToString());

                foreach (string itemKey in gameDataJson[0]["upgradeCosts"].Keys)
                {
                    baseCost.upgradeCosts[itemKey] = int.Parse(gameDataJson[0]["upgradeCosts"][itemKey].ToString());
                }
                foreach (string itemKey in gameDataJson[0]["playerData"].Keys)
                {
                    baseCost.playerData[itemKey] = float.Parse(gameDataJson[0]["playerData"][itemKey].ToString());
                }
                foreach (string itemKey in gameDataJson[0]["employeeData"].Keys)
                {
                    baseCost.employeeData[itemKey] = float.Parse(gameDataJson[0]["employeeData"][itemKey].ToString());
                }
                foreach (string itemKey in gameDataJson[0]["objectData"].Keys)
                {
                    baseCost.objectData[itemKey] = int.Parse(gameDataJson[0]["objectData"][itemKey].ToString());
                }
                foreach (string itemKey in gameDataJson[0]["gameProgressBool"].Keys)
                {
                    baseCost.gameProgressBool[itemKey] = bool.Parse(gameDataJson[0]["gameProgressBool"][itemKey].ToString());
                }

                foreach (LitJson.JsonData equip in gameDataJson[0]["employeeList"])
                {
                    baseCost.employeeList.Add(equip.ToString());
                }

                Debug.Log(baseCost.ToString());
            }
        }
        else
        {
            Debug.LogError("���� ���� ��ȸ�� �����߽��ϴ�. : " + bro);
        }
    }

    public void GameDataUpdate()
    {
        // Step 4. ���� ���� ���� �����ϱ�
        if (baseCost == null)
        {
            Debug.LogError("�������� �ٿ�ްų� ���� ������ �����Ͱ� �������� �ʽ��ϴ�. Insert Ȥ�� Get�� ���� �����͸� �������ּ���.");
            return;
        }

        ObjStackCountSave();
        Param param = new Param();
        param.Add("upgradeCosts", baseCost.upgradeCosts);
        param.Add("playerData", baseCost.playerData);
        param.Add("employeeList", baseCost.employeeList);
        param.Add("employeeData", baseCost.employeeData);
        param.Add("objectData", baseCost.objectData);
        param.Add("gameProgressBool", baseCost.gameProgressBool);
        param.Add("guideStep", baseCost.guideStep);

        BackendReturnObject bro = null;

        if (string.IsNullOrEmpty(gameDataRowInDate))
        {
            Debug.Log("�� ���� �ֽ� ���� ���� ������ ������ ��û�մϴ�.");

            bro = Backend.GameData.Update("UserData", new Where(), param);
        }
        else
        {
            Debug.Log($"{gameDataRowInDate}�� ���� ���� ������ ������ ��û�մϴ�.");

            bro = Backend.GameData.UpdateV2("UserData", gameDataRowInDate, Backend.UserInDate, param);
        }

        if (bro.IsSuccess())
        {
            Debug.Log("���� ���� ������ ������ �����߽��ϴ�. : " + bro);
        }
        else
        {
            Debug.LogError("���� ���� ������ ������ �����߽��ϴ�. : " + bro);
        }
    }
}


