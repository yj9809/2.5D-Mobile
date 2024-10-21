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
    private AppUpdateInfo appUpdateInfoResult;

    private void Start()
    {
        textPanel.SetActive(false);

#if UNITY_EDITOR
        LogMessage("�ξ� ������Ʈ�� �����Ϳ�����\n�������� �ʽ��ϴ�.");
        backendManager.GuestLogin();
#else
        appUpdateManager = new AppUpdateManager();
        StartCoroutine(CheckForUpdate());
#endif
    }

    private IEnumerator CheckForUpdate()
    {
        yield return new WaitForSeconds(0.5f);

        var appUpdateInfoOperation = appUpdateManager.GetAppUpdateInfo();
        yield return appUpdateInfoOperation;

        if (appUpdateInfoOperation.IsSuccessful)
        {
            appUpdateInfoResult = appUpdateInfoOperation.GetResult();

            if (appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateAvailable)
            {
                LogMessage("������Ʈ�� �ʿ��մϴ�.\n�ڵ����� ������Ʈ�� �����մϴ�.");
                yield return StartCoroutine(StartUpdate());
            }
            else if (appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateNotAvailable)
            {
                LogMessage("������Ʈ ����");
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

    private IEnumerator StartUpdate()
    {
        var appUpdateOptions = AppUpdateOptions.ImmediateAppUpdateOptions();
        var startUpdateRequest = appUpdateManager.StartUpdate(appUpdateInfoResult, appUpdateOptions);
        yield return startUpdateRequest;

        if (startUpdateRequest.IsDone && startUpdateRequest.Status == AppUpdateStatus.Downloading)
        {
            while (!startUpdateRequest.IsDone)
            {
                LogMessage("������Ʈ �ٿ�ε尡 ���� ���Դϴ�...");
                yield return null;
            }

            var result = appUpdateManager.CompleteUpdate();
            while (!result.IsDone)
            {
                yield return new WaitForEndOfFrame();
            }
            LogMessage("������Ʈ�� ���������� �Ϸ�Ǿ����ϴ�.");
        }
        else if (startUpdateRequest.Status == AppUpdateStatus.Failed)
        {
            LogMessage("������Ʈ ����: " + startUpdateRequest.Error);
        }
    }

    private void LogMessage(string message)
    {
        logText.text = "";
        Debug.LogWarning(message);
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
