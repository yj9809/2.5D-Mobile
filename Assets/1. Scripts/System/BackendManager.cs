using BackEnd;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.UI;

public class BackendManager : MonoBehaviour
{
    [SerializeField] private LoadingManager loadingManager;
    [SerializeField] private Image image;

    void Awake()
    {
        var bro = Backend.Initialize();

        if (bro.IsSuccess())
        {
            Debug.Log("초기화 성공 : " + bro);
        }
        else
        {
            Debug.LogError("초기화 실패 : " + bro);
        }
        //Backend.BMember.DeleteGuestInfo();
        GuestLogin();
    }

    private void GuestLogin()
    {
        Backend.BMember.GuestLogin("게스트 로그인으로 로그인함", (callback) => {
            if (callback.IsSuccess())
            {
                Debug.Log("게스트 로그인에 성공했습니다");
                DataManager.Instance.GameDataGet();

                if (DataManager.Instance.baseCost == null)
                {
                    DataManager.Instance.GameDataInsert();
                }
                loadingManager.StartCoroutine();
            }
            else
            {
                image.gameObject.SetActive(true);
                string googlehash = Backend.Utils.GetGoogleHash();
                string errorCode = callback.GetErrorCode().ToString(); // 오류 코드 가져오기
                string errorMessage = callback.GetMessage(); // 오류 메시지 가져오기
            }
        });
    }
}
