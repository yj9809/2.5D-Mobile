using BackEnd;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class BackendManager : MonoBehaviour
{
    void Start()
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
    }
}
