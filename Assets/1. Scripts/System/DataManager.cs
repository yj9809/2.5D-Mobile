using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using BackEnd;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

// 각종 스택, 종업원 정보를 담기 위한 인터페이스
public interface IObjectDataSave
{
    void ObjectDataSave();
}

// 플레이 시 저장 값 확인 용 Base 데이터 
// 데이터 추가 할 경우 딕셔너리<string, (int & float & bool)> 값 활용하여 만들고 
// 밑에 있는 dataupdata 함수에서 param 값도 추가해줘야함
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

    // Player 데이터
    public Dictionary<string, float> playerData = new Dictionary<string, float>
    {   
        { "baseSpeed", 5 },
        { "baseCartSpeed", 2.5f },
        { "maxObjStackCount", 3 },
        { "gold", 100},
        { "goldPerBox", 50 }
    };

    public List<string> employeeList = new List<string>();

    // Employee 데이터
    public Dictionary<string, float> employeeData = new Dictionary<string, float>
    {
        { "employeeSpeed", 3 },
        { "employeeCartSpeed", 1.5f },
        { "employeeMaxObjStackCount", 3 }
    };

    // 오브젝트 데이터
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
    // 데이터 추가
    public void GameDataInsert()
    {
        if (baseCost == null)
            baseCost = new BaseCost();

        baseCost.guestID = Backend.BMember.GetGuestID();

        // 데이터를 추가할 경우 위쪽 BaseCost 클래스에서 딕셔너리로 추가 후
        // 해당 부분 param.Add를 통해 정보를 추가해 줘야함
        // 불러오는 부분에서도 각종 정보를 직접 넣어주는 코드를 작성해야함
        // 불러오기 부분에서 따로 주석 처리 해두겠음
        Param param = new Param();
        param.Add("guestID", baseCost.guestID);
        param.Add("upgradeCosts", baseCost.upgradeCosts);
        param.Add("playerData", baseCost.playerData);
        param.Add("employeeList", baseCost.employeeList);
        param.Add("employeeData", baseCost.employeeData);
        param.Add("objectData", baseCost.objectData);
        param.Add("gameProgressBool", baseCost.gameProgressBool);
        param.Add("guideStep", baseCost.guideStep);

        Debug.Log("게임 정보 데이터 삽입을 요청합니다.");
        var bro = Backend.GameData.Insert("UserData", param);

        if (bro.IsSuccess())
        {
            Debug.Log("게임 정보 데이터 삽입에 성공했습니다. : " + bro);

            //삽입한 게임 정보의 고유값입니다.  
            gameDataRowInDate = bro.GetInDate();
        }
        else
        {
            GameDataInsert();
            Debug.LogError("게임 정보 데이터 삽입에 실패했습니다. : " + bro);
        }
    }

    // 데이터가 존재 할 경우 데이터 가져오기
    public void GameDataGet()
    {
        Debug.Log("게임 정보 조회 함수를 호출합니다.");

        var bro = Backend.GameData.GetMyData("UserData", new Where());

        if (bro.IsSuccess())
        {
            Debug.Log("게임 정보 조회에 성공했습니다. : " + bro);


            LitJson.JsonData gameDataJson = bro.FlattenRows(); // Json으로 리턴된 데이터를 받아옵니다.  

            // 받아온 데이터의 갯수가 0이라면 데이터가 존재하지 않는 것입니다.  
            if (gameDataJson.Count <= 0)
            {
                Debug.LogWarning("데이터가 존재하지 않습니다.");
            }
            else
            {
                gameDataRowInDate = gameDataJson[0]["inDate"].ToString(); //불러온 게임 정보의 고유값입니다.  

                baseCost = new BaseCost();

                baseCost.guideStep = int.Parse(gameDataJson[0]["guideStep"].ToString());
                baseCost.guestID = gameDataJson[0]["guestID"].ToString();

                //데이터 추가할 경우 해당 부분에 반복문을 통해 데이터 정보를 넣어줘야함
                // 밑에 foreach 형식이나 for 문 사용하여 해당 형식으로 해당 데이터를 잘 넣어줘야함
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
            Debug.LogError("게임 정보 조회에 실패했습니다. : " + bro);
        }
    }

    // 게임 정보를 업데이트 하는 함수
    public void GameDataUpdate()
    {
        // Step 4. 게임 정보 수정 구현하기
        if (baseCost == null)
        {
            Debug.LogError("서버에서 다운받거나 새로 삽입한 데이터가 존재하지 않습니다. Insert 혹은 Get을 통해 데이터를 생성해주세요.");
            return;
        }

        // 해당 부분도 마찬가지로 데이터 추가할 때 
        // 밑 param.Add로 해당 딕셔너리를 제대로 추가해줘야함
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
            Debug.Log("내 제일 최신 게임 정보 데이터 수정을 요청합니다.");

            bro = Backend.GameData.Update("UserData", new Where(), param);
        }
        else
        {
            Debug.Log($"{gameDataRowInDate}의 게임 정보 데이터 수정을 요청합니다.");

            bro = Backend.GameData.UpdateV2("UserData", gameDataRowInDate, Backend.UserInDate, param);
        }

        if (bro.IsSuccess())
        {
            Debug.Log("게임 정보 데이터 수정에 성공했습니다. : " + bro);
        }
        else
        {
            Debug.LogError("게임 정보 데이터 수정에 실패했습니다. : " + bro);
        }
    }

    // 데이터 삭제 함수
    public void DeleteData()
    {
        BackendReturnObject bro = Backend.GameData.DeleteV2("UserData", gameDataRowInDate, Backend.UserInDate);
        
        // 데이터를 삭제하고 게임을 꺼버림
        if(bro.IsSuccess())
        {
            Debug.Log("정보 삭제 완료");
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
        else
        {
            Debug.Log("삭제 실패");
        }
    }
}


