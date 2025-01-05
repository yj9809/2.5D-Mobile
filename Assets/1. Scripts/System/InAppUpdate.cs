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
        LogMessage("인앱 업데이트는 에디터에서는\n지원되지 않습니다.");
#else
StartCoroutine(Init());
#endif
    }
    private IEnumerator Init()
    {
        yield return new WaitForSeconds(0.5f);

        try
        {
            LogMessage("실행");
            appUpdateManager = new AppUpdateManager();
            LogMessage("앱 업데이트 매니저 참조 성공");
            StartCoroutine(CheckForUpdate());
            LogMessage("앱 업데이트 코루틴 실행 성공");
        }
        catch(Exception err)
        {
            LogMessage(err.Message);
        }
    }
    private IEnumerator CheckForUpdate()
    {
        LogMessage("업데이트 정보를 가져오는 중...");
        PlayAsyncOperation<AppUpdateInfo, AppUpdateErrorCode> appUpdateInfoOperation =
            appUpdateManager.GetAppUpdateInfo();
        yield return appUpdateInfoOperation;

        if (appUpdateInfoOperation.IsSuccessful)
        {
            var appUpdateInfoResult = appUpdateInfoOperation.GetResult();
            LogMessage("업데이트 정보 수신 성공");

            if (appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateAvailable)
            {
                LogMessage("업데이트가 필요합니다.\n자동으로 업데이트를 진행합니다.");
                var appUpdateOptions = AppUpdateOptions.FlexibleAppUpdateOptions();
                var startUpdateRequest = appUpdateManager.StartUpdate(appUpdateInfoResult, appUpdateOptions);

                while (!startUpdateRequest.IsDone)
                {
                    if (startUpdateRequest.Status == AppUpdateStatus.Downloading)
                    {
                        LogMessage("업데이트 다운로드가 진행 중입니다...");
                    }
                    else if (startUpdateRequest.Status == AppUpdateStatus.Downloaded)
                    {
                        LogMessage("업데이트 다운로드가 완료되었습니다 !");
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
                LogMessage("환영합니다 !"); // 업데이트 없을 시
                yield return (int)UpdateAvailability.UpdateNotAvailable;
            }
            else
            {
                LogMessage("업데이트 상태: " + appUpdateInfoResult.UpdateAvailability);
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
