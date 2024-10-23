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
        TheBackend.ToolKit.GoogleLogin.Android.GoogleLogin(true, GoogleLoginCallback);
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
