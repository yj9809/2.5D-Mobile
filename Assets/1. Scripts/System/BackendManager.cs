using BackEnd;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.UI;

public class BackendManager : MonoBehaviour
{
    [SerializeField] private LoadingManager loadingManager;
    [SerializeField] private Image image;

    // 게임 실행 시 뒤끝 서버 초기화
    void Awake()
    {
        var bro = Backend.Initialize();
    }

    // 구글 로그인 함수
    public void StartGoogleLogin()
    {
        PlayGamesPlatform.Activate();
        //TheBackend.ToolKit.GoogleLogin.Android.GoogleLogin(true, GoogleLoginCallback);
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }
    void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            GetAccessCode();
            // Continue with Play Games Services
        }
        else
        {
            PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
            // Play Games Services와의 통합을 비활성화하거나
            // 사용자에게 로그인을 요청하는 버튼을 표시하세요.
            // 버튼 클릭 시 아래를 호출해야 합니다:
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).

        }
    }

    public void GetAccessCode()
    {
        PlayGamesPlatform.Instance.RequestServerSideAccess(
          /* forceRefreshToken= */ false,
          code => {
              Debug.Log("구글 인증 코드 : " + code);
              try
              {
                  Backend.BMember.GetGPGS2AccessToken(code, googleCallback =>
                  {
                      Debug.Log("GetGPGS2AccessToken 함수 호출 결과 " + googleCallback);

                      string accessToken = "";

                      if (googleCallback.IsSuccess())
                      {
                          accessToken = googleCallback.GetReturnValuetoJSON()["access_token"].ToString();
                          TheBackend.ToolKit.GoogleLogin.Android.GoogleLogin(true, GoogleLoginCallback);
                          loadingManager.StartCoroutine();
                      }
                  });
              }
              catch (System.Exception err)
              {
                  Debug.LogError(err);
              }
          });
    }

    // 구글 로그인이 가능한지 확인 후 결과 반환
    private void GoogleLoginCallback(bool isSuccess, string errorMessage, string token)
    {
        if (isSuccess == false)
        {
            return;
        }

        var bro = Backend.BMember.AuthorizeFederation(token, FederationType.Google);

        try
        {
            DataManager.Instance.GameDataGet();
            if (DataManager.Instance.baseCost == null)
            {
                DataManager.Instance.GameDataInsert();
            }

        }
        catch (System.Exception err)
        {
            Debug.LogError(err);
        }
        loadingManager.StartCoroutine();
    }

    // 게스트 로그인 함수
    // 실제 출시 후에는 활용할 일이 없어 삭제 예정
    public void GuestLogin()
    {
        Backend.BMember.GuestLogin("게스트 로그인으로 로그인함", (callback) => {
            if (callback.IsSuccess())
            {
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
