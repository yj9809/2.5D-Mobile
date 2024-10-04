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
        //GuestLogin();
        StartGoogleLogin();
    }

    public void StartGoogleLogin()
    {
        TheBackend.ToolKit.GoogleLogin.Android.GoogleLogin(true, GoogleLoginCallback);
    }

    private void GoogleLoginCallback(bool isSuccess, string errorMessage, string token)
    {
        if (isSuccess == false)
        {
            Debug.LogError(errorMessage);
            return;
        }

        Debug.Log("구글 토큰 : " + token);
        var bro = Backend.BMember.AuthorizeFederation(token, FederationType.Google);
        DataManager.Instance.GameDataGet();
        if (DataManager.Instance.baseCost == null)
        {
            DataManager.Instance.GameDataInsert();
        }

        loadingManager.StartCoroutine();

        Debug.Log("페데레이션 로그인 결과 : " + bro);
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
