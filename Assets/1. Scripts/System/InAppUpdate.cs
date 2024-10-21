using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Play.Common;
using Google.Play.AppUpdate;

public class InAppUpdate : MonoBehaviour
{
    private AppUpdateManager appUpdateManager;

    private void Start()
    {
#if UNITY_EDITOR
        Debug.LogWarning("인앱 업데이트는 에디터에서는 지원되지 않습니다.");
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
                        Debug.LogWarning("업데이트 다운로드가 진행 중입니다...");
                    }
                    else if (startUpdateRequest.Status == AppUpdateStatus.Downloaded)
                    {
                        Debug.LogWarning("업데이트가 다운로드 완료되었습니다 !");
                    }
                    yield return null;
                }
                var result = appUpdateManager.CompleteUpdate();
                while (!result.IsDone)
                {
                    yield return new WaitForEndOfFrame();
                }
                Debug.Log("업데이트가 성공적으로 완료되었습니다.");
                // 필요 시 앱 재시작 또는 추가 처리
            }
            else if (appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateNotAvailable)
            {
                Debug.LogWarning("업데이트 없음");
            }
            else
            {
                Debug.LogWarning("업데이트 상태 알 수 없음");
            }
        }
        else
        {
            Debug.LogError($"In-App Update Error: {appUpdateInfoOperation.Error}");
        }
    }
}
