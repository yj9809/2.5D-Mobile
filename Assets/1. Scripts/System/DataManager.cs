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
        { "baseSpeedUpgradeCost", 500 },
        { "baseMaxObjStackCountUpgradeCost", 500 },
        { "baseGoldPerBoxUpgradeCost", 5000 },
        { "baseEmployeeSpeedUpgradeCost", 500 },
        { "baseEmployeeMaxObjStackCountUpgradeCost", 500 },
        { "baseEmployeeAddCost", 5000 },
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
        { "packagingBoxStorageStackCount", 0 },
        { "truckBoxStackCount", 0 }
    };

    public Dictionary<string, bool> gameProgressBool = new Dictionary<string, bool>
    {
        { "Office", false },
        { "Container1", false },
        { "Machine1", false },
        { "Container2", false },
        { "Machine2", false },
        { "Stall", false },
        { "Store", false }
    };
    public int guideStep = 0;
    public bool newGame = true;
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
    #region ���� ������ ����� �Լ���
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
        param.Add("newGame", baseCost.newGame);

        var bro = Backend.GameData.Insert("TestUserData", param);

        if (bro.IsSuccess())
        {
            //������ ���� ������ �������Դϴ�.  
            gameDataRowInDate = bro.GetInDate();
        }
        else
        {
            GameDataInsert();
        }
    }

    // �����Ͱ� ���� �� ��� ������ ��������
    public void GameDataGet()
    {
        var bro = Backend.GameData.GetMyData("TestUserData", new Where());

        if (bro.IsSuccess())
        {
            LitJson.JsonData gameDataJson = bro.FlattenRows(); // Json���� ���ϵ� �����͸� �޾ƿɴϴ�.  

            // �޾ƿ� �������� ������ 0�̶�� �����Ͱ� �������� �ʴ� ���Դϴ�. 
            if(gameDataJson.Count > 0)
            {
                gameDataRowInDate = gameDataJson[0]["inDate"].ToString(); //�ҷ��� ���� ������ �������Դϴ�.  

                baseCost = new BaseCost();

                baseCost.guideStep = int.Parse(gameDataJson[0]["guideStep"].ToString());
                baseCost.newGame = bool.Parse(gameDataJson[0]["newGame"].ToString());
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
            }
        }
    }

    // ���� ������ ������Ʈ �ϴ� �Լ�
    public void GameDataUpdate()
    {
        if (baseCost == null)
        {
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
        param.Add("newGame", baseCost.newGame);

        BackendReturnObject bro = null;

        if (string.IsNullOrEmpty(gameDataRowInDate))
        {
            bro = Backend.GameData.Update("TestUserData", new Where(), param);
        }
        else
        {
            bro = Backend.GameData.UpdateV2("TestUserData", gameDataRowInDate, Backend.UserInDate, param);
        }
    }

    // ������ ���� �Լ�
    public void DeleteData()
    {
        BackendReturnObject bro = Backend.GameData.DeleteV2("TestUserData", gameDataRowInDate, Backend.UserInDate);
        
        // �����͸� �����ϰ� ������ ������
        if(bro.IsSuccess())
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
    #endregion
}


