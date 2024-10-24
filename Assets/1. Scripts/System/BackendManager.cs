using BackEnd;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.UI;

public class BackendManager : MonoBehaviour
{
    [SerializeField] private LoadingManager loadingManager;
    [SerializeField] private Image image;

    // ���� ���� �� �ڳ� ���� �ʱ�ȭ
    void Awake()
    {
        var bro = Backend.Initialize();
    }

    // ���� �α��� �Լ�
    public void StartGoogleLogin()
    {
        PlayGamesPlatform.Activate();
        TheBackend.ToolKit.GoogleLogin.Android.GoogleLogin(true, GoogleLoginCallback);
        //PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
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
            Debug.Log("����");
            loadingManager.StartCoroutine();
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
        }
    }

    public void GetAccessCode()
    {
        PlayGamesPlatform.Instance.RequestServerSideAccess(
          /* forceRefreshToken= */ false,
          code => {
              Debug.Log("���� ���� �ڵ� : " + code);
              try
              {
                  Backend.BMember.GetGPGS2AccessToken(code, googleCallback =>
                  {
                      Debug.Log("GetGPGS2AccessToken �Լ� ȣ�� ��� " + googleCallback);

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

    // ���� �α����� �������� Ȯ�� �� ��� ��ȯ
    private void GoogleLoginCallback(bool isSuccess, string errorMessage, string token)
    {
        if (isSuccess == false)
        {
            return;
        }

        var bro = Backend.BMember.AuthorizeFederation(token, FederationType.Google);
        DataManager.Instance.GameDataGet();
        if (DataManager.Instance.baseCost == null)
        {
            DataManager.Instance.GameDataInsert();
        }

        loadingManager.StartCoroutine();
    }

    // �Խ�Ʈ �α��� �Լ�
    // ���� ��� �Ŀ��� Ȱ���� ���� ���� ���� ����
    public void GuestLogin()
    {
        Backend.BMember.GuestLogin("�Խ�Ʈ �α������� �α�����", (callback) => {
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
                string errorCode = callback.GetErrorCode().ToString(); // ���� �ڵ� ��������
                string errorMessage = callback.GetMessage(); // ���� �޽��� ��������
            }
        });
    }
}
