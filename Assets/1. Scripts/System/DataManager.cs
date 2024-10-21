using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using BackEnd;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

// ���� ����, ������ ������ ��� ���� �������̽�
public interface IObjectDataSave
{
    void ObjectDataSave();
}

// �÷��� �� ���� �� Ȯ�� �� Base ������ 
// ������ �߰� �� ��� ��ųʸ�<string, (int & float & bool)> �� Ȱ���Ͽ� ����� 
// �ؿ� �ִ� dataupdata �Լ����� param ���� �߰��������
public class BaseCost
{
    public string guestID;

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

    // Player ������
    public Dictionary<string, float> playerData = new Dictionary<string, float>
    {   
        { "baseSpeed", 5 },
        { "baseCartSpeed", 2.5f },
        { "maxObjStackCount", 3 },
        { "gold", 100},
        { "goldPerBox", 50 }
    };

    public List<string> employeeList = new List<string>();

    // Employee ������
    public Dictionary<string, float> employeeData = new Dictionary<string, float>
    {
        { "employeeSpeed", 3 },
        { "employeeCartSpeed", 1.5f },
        { "employeeMaxObjStackCount", 3 }
    };

    // ������Ʈ ������
    public Dictionary<string, int> objectData = new Dictionary<string, int>
    {
        { "conveyorBeltBoxStorageStackCount", 0 },
        { "churuStorageStackCount", 0 },
        { "packagingWaitObjCount", 0 },
        { "packagingBoxStorageStackCount", 0 }
    };

    public Dictionary<string, bool> gameProgressBool = new Dictionary<string, bool>
    {
        { "NewGame", false},
        { "Office", false },
        { "Container1", false },
        { "Machine1", false },
        { "Container2", false },
        { "Machine2", false },
        { "Stall", false },
        { "Store", false }
    };
    public int guideStep = 0;
}

public class DataManager : Singleton<DataManager>
{
    public BaseCost baseCost;

    private List<IObjectDataSave> objectDataList = new List<IObjectDataSave>();

    public string fileName = "SaveFile";

    private string gameDataRowInDate = string.Empty;

    // Start is called before the first frame update

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
    // ������ �߰�
    public void GameDataInsert()
    {
        if (baseCost == null)
            baseCost = new BaseCost();

        baseCost.guestID = Backend.BMember.GetGuestID();

        // �����͸� �߰��� ��� ���� BaseCost Ŭ�������� ��ųʸ��� �߰� ��
        // �ش� �κ� param.Add�� ���� ������ �߰��� �����
        // �ҷ����� �κп����� ���� ������ ���� �־��ִ� �ڵ带 �ۼ��ؾ���
        // �ҷ����� �κп��� ���� �ּ� ó�� �صΰ���
        Param param = new Param();
        param.Add("guestID", baseCost.guestID);
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

    // �����Ͱ� ���� �� ��� ������ ��������
    public void GameDataGet()
    {
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
                baseCost.guestID = gameDataJson[0]["guestID"].ToString();

                //������ �߰��� ��� �ش� �κп� �ݺ����� ���� ������ ������ �־������
                // �ؿ� foreach �����̳� for �� ����Ͽ� �ش� �������� �ش� �����͸� �� �־������
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

    // ���� ������ ������Ʈ �ϴ� �Լ�
    public void GameDataUpdate()
    {
        // Step 4. ���� ���� ���� �����ϱ�
        if (baseCost == null)
        {
            Debug.LogError("�������� �ٿ�ްų� ���� ������ �����Ͱ� �������� �ʽ��ϴ�. Insert Ȥ�� Get�� ���� �����͸� �������ּ���.");
            return;
        }

        // �ش� �κе� ���������� ������ �߰��� �� 
        // �� param.Add�� �ش� ��ųʸ��� ����� �߰��������
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

    // ������ ���� �Լ�
    public void DeleteData()
    {
        BackendReturnObject bro = Backend.GameData.DeleteV2("UserData", gameDataRowInDate, Backend.UserInDate);
        
        // �����͸� �����ϰ� ������ ������
        if(bro.IsSuccess())
        {
            Debug.Log("���� ���� �Ϸ�");
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
        else
        {
            Debug.Log("���� ����");
        }
    }
}


