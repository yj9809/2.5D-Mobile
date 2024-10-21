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
        LogMessage("인앱 업데이트는 에디터에서는\n지원되지 않습니다.");
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
            var appUpdateInfoResult = appUpdateInfoOperation.GetResult();
            // 업데이트 가능한 상태
            if (appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateAvailable)
            {
                // 유연한 업데이트
                var appUpdateOptions = AppUpdateOptions.FlexibleAppUpdateOptions();
                var startUpdateRequest = appUpdateManager.StartUpdate(appUpdateInfoResult, appUpdateOptions);

                // 즉시 업데이트
                /* var appUpdateOptions = AppUpdateOptions.ImmediateAppUpdateOptions();
                var startUpdateRequest = appUpdateManager.StartUpdate(appUpdateInfoResult, appUpdateOptions);
                yield return startUpdateRequest; */

                while (!startUpdateRequest.IsDone)
                {
                    if (startUpdateRequest.Status == AppUpdateStatus.Downloading)
                    {
                        LogMessage("업데이트 다운로드가 진행 중입니다...");
                    }
                    else if (startUpdateRequest.Status == AppUpdateStatus.Downloaded)
                    {
                        LogMessage("업데이트가 다운로드 완료되었습니다 !");
                    }
                    yield return null;
                }
                var result = appUpdateManager.CompleteUpdate();
                while (!result.IsDone)
                {
                    yield return new WaitForEndOfFrame();
                }
                LogMessage("업데이트가 성공적으로 완료되었습니다.");
                // 필요 시 앱 재시작 또는 추가 처리
            }
            else if (appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateNotAvailable)
            {
                LogMessage("업데이트 없음");
            }
            else
            {
                LogMessage("업데이트 상태 알 수 없음");
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

    // 로그 메시지를 출력하는 메서드
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
