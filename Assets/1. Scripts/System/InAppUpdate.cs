using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Play.Common;
using Google.Play.AppUpdate;

public class InAppUpdate : MonoBehaviour
{
    AppUpdateManager appUpdateManager = new AppUpdateManager();

    private void Start()
    {
        #if UNITY_EDITOR
        #else
            StartCoroutine(CheckForUpdate());
        #endif
    }

    private IEnumerator CheckForUpdate()
    {
        appUpdateManager = new AppUpdateManager();

        PlayAsyncOperation<AppUpdateInfo, AppUpdateErrorCode> appUpdateInfoOperation =
            appUpdateManager.GetAppUpdateInfo();

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
                        Debug.Log("업데이트 다운로드가 진행 중입니다...");
                    }
                    else if (startUpdateRequest.Status == AppUpdateStatus.Downloaded)
                    {
                        Debug.Log("업데이트가 다운로드 완료되었습니다 !");
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
            // 업데이트 없을 경우
            else if (appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateNotAvailable)
            {
                Debug.Log("ㅋㅋ없데이트");
                yield return (int)UpdateAvailability.UpdateNotAvailable;
            }
            else
            {
                Debug.Log("몰?루");
                yield return (int)UpdateAvailability.Unknown;
            }
        }
        else
        {
            // Log appUpdateInfoOperation.Error.
            Debug.LogError("In_AppUptates 스크립트 에러");
        }
    }
}
