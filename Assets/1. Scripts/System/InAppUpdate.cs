using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Play.Common;
using Google.Play.AppUpdate;
using TMPro;

public class InAppUpdate : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI logText;
    [SerializeField] private GameObject textPanel;

    [Header("Manager")]
    [SerializeField] private BackendManager backendManager;
    private AppUpdateManager appUpdateManager;

    private void Start()
    {
        textPanel.SetActive(false);
        
#if UNITY_EDITOR
        backendManager.GuestLogin();
        LogMessage("�ξ� ������Ʈ�� �����Ϳ�����\n�������� �ʽ��ϴ�.");
#else
StartCoroutine(Init());
#endif
    }
    private IEnumerator Init()
    {
        yield return new WaitForSeconds(0.5f);

        try
        {
            LogMessage("����");
            appUpdateManager = new AppUpdateManager();
            LogMessage("�� ������Ʈ �Ŵ��� ���� ����");
            StartCoroutine(CheckForUpdate());
            LogMessage("�� ������Ʈ �ڷ�ƾ ���� ����");
        }
        catch(Exception err)
        {
            LogMessage(err.Message);
        }
    }
    private IEnumerator CheckForUpdate()
    {
        LogMessage("������Ʈ ������ �������� ��...");
        PlayAsyncOperation<AppUpdateInfo, AppUpdateErrorCode> appUpdateInfoOperation =
            appUpdateManager.GetAppUpdateInfo();
        yield return appUpdateInfoOperation;

        if (appUpdateInfoOperation.IsSuccessful)
        {
            var appUpdateInfoResult = appUpdateInfoOperation.GetResult();
            LogMessage("������Ʈ ���� ���� ����");

            if (appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateAvailable)
            {
                LogMessage("������Ʈ�� �ʿ��մϴ�.\n�ڵ����� ������Ʈ�� �����մϴ�.");
                var appUpdateOptions = AppUpdateOptions.FlexibleAppUpdateOptions();
                var startUpdateRequest = appUpdateManager.StartUpdate(appUpdateInfoResult, appUpdateOptions);

                while (!startUpdateRequest.IsDone)
                {
                    if (startUpdateRequest.Status == AppUpdateStatus.Downloading)
                    {
                        LogMessage("������Ʈ �ٿ�ε尡 ���� ���Դϴ�...");
                    }
                    else if (startUpdateRequest.Status == AppUpdateStatus.Downloaded)
                    {
                        LogMessage("������Ʈ �ٿ�ε尡 �Ϸ�Ǿ����ϴ� !");
                    }
                    yield return null;
                }
                var result = appUpdateManager.CompleteUpdate();
                while (!result.IsDone)
                {
                    yield return new WaitForEndOfFrame();
                }
                yield return (int)startUpdateRequest.Status;
            }
            else if (appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateNotAvailable)
            {
                LogMessage("ȯ���մϴ� !"); // ������Ʈ ���� ��
                yield return (int)UpdateAvailability.UpdateNotAvailable;
            }
            else
            {
                LogMessage("������Ʈ ����: " + appUpdateInfoResult.UpdateAvailability);
                yield return (int)UpdateAvailability.Unknown;
            }
        }
        else
        {
            LogMessage($"In-App Update Error: {appUpdateInfoOperation.Error}");
        }

#if !UNITY_EDITOR
        backendManager.StartGoogleLogin();
#endif
    }

    private void LogMessage(string message)
    {
        logText.text = "";
        if (logText != null && textPanel != null)
        {
            textPanel.SetActive(true);
            logText.text += message + "\n";
        }
    }

    private void Update()
    {
        if (logText != null && textPanel != null)
        {
            if (string.IsNullOrEmpty(logText.text))
            {
                textPanel.SetActive(false);
            }
        }
    }
}
