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
            Debug.Log("�ʱ�ȭ ���� : " + bro);
        }
        else
        {
            Debug.LogError("�ʱ�ȭ ���� : " + bro);
        }
        //Backend.BMember.DeleteGuestInfo();
        GuestLogin();
    }

    private void GuestLogin()
    {
        Backend.BMember.GuestLogin("�Խ�Ʈ �α������� �α�����", (callback) => {
            if (callback.IsSuccess())
            {
                Debug.Log("�Խ�Ʈ �α��ο� �����߽��ϴ�");
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
