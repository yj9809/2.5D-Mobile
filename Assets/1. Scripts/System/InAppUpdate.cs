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
            appUpdateInfoResult = appUpdateInfoOperation.GetResult();

            if (appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateAvailable)
            {
                LogMessage("업데이트가 필요합니다.\n자동으로 업데이트를 진행합니다.");
                yield return StartCoroutine(StartUpdate());
            }
            else if (appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateNotAvailable)
            {
                LogMessage("업데이트 없음");
                backendManager.StartGoogleLogin();
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
        var appUpdateOptions = AppUpdateOptions.FlexibleAppUpdateOptions(); // 유연한 업데이트 옵션
        var startUpdateRequest = appUpdateManager.StartUpdate(appUpdateInfoResult, appUpdateOptions);

        LogMessage("업데이트가 시작되었습니다. 다운로드 진행 중입니다...");

        // 다운로드 진행 중 상태 확인
        while (!startUpdateRequest.IsDone)
        {
            yield return null; // 프레임 대기
        }

        if (startUpdateRequest.Status == AppUpdateStatus.Failed)
        {
            LogMessage("업데이트 실패: " + startUpdateRequest.Error);
        }
        else if (startUpdateRequest.Status == AppUpdateStatus.Downloading)
        {
            // 다운로드 완료 후 처리
            var result = appUpdateManager.CompleteUpdate();
            while (!result.IsDone)
            {
                yield return new WaitForEndOfFrame();
            }
            LogMessage("업데이트가 성공적으로 완료되었습니다.");
            Application.Quit();
        }
    }

    private void RestartApp()
    {
        LogMessage("앱을 재시작합니다...");
        // 현재 씬을 다시 로드하여 앱을 재시작합니다.
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
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
