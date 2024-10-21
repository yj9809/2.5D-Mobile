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
        Debug.LogWarning("�ξ� ������Ʈ�� �����Ϳ����� �������� �ʽ��ϴ�.");
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
            // ������Ʈ ������ ����
            if (appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateAvailable)
            {
                // ������ ������Ʈ
                var appUpdateOptions = AppUpdateOptions.FlexibleAppUpdateOptions();
                var startUpdateRequest = appUpdateManager.StartUpdate(appUpdateInfoResult, appUpdateOptions);

                // ��� ������Ʈ
                /* var appUpdateOptions = AppUpdateOptions.ImmediateAppUpdateOptions();
                var startUpdateRequest = appUpdateManager.StartUpdate(appUpdateInfoResult, appUpdateOptions);
                yield return startUpdateRequest; */

                while (!startUpdateRequest.IsDone)
                {
                    if (startUpdateRequest.Status == AppUpdateStatus.Downloading)
                    {
                        Debug.LogWarning("������Ʈ �ٿ�ε尡 ���� ���Դϴ�...");
                    }
                    else if (startUpdateRequest.Status == AppUpdateStatus.Downloaded)
                    {
                        Debug.LogWarning("������Ʈ�� �ٿ�ε� �Ϸ�Ǿ����ϴ� !");
                    }
                    yield return null;
                }
                var result = appUpdateManager.CompleteUpdate();
                while (!result.IsDone)
                {
                    yield return new WaitForEndOfFrame();
                }
                Debug.Log("������Ʈ�� ���������� �Ϸ�Ǿ����ϴ�.");
                // �ʿ� �� �� ����� �Ǵ� �߰� ó��
            }
            else if (appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateNotAvailable)
            {
                Debug.LogWarning("������Ʈ ����");
            }
            else
            {
                Debug.LogWarning("������Ʈ ���� �� �� ����");
            }
        }
        else
        {
            Debug.LogError($"In-App Update Error: {appUpdateInfoOperation.Error}");
        }
    }
}
