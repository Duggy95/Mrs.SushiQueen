using System.Collections;
using UnityEngine;
using Google.Play.Common;
using Google.Play.AppUpdate;

public class GooglePlayManager : MonoBehaviour
{
    AppUpdateManager appUpdateManager = null;

    public void Awake()
    {
        StartCoroutine(CheckForUpdate());
    }

    IEnumerator CheckForUpdate()
    {
        appUpdateManager = new AppUpdateManager();
        PlayAsyncOperation<AppUpdateInfo, AppUpdateErrorCode> appUpdateInfoOperation = appUpdateManager.GetAppUpdateInfo();

        yield return appUpdateInfoOperation;

        if (appUpdateInfoOperation.IsSuccessful)
        {
            var appUpdateInfoResult = appUpdateInfoOperation.GetResult();

            if (appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateAvailable)
            {
            }
            else
            {

            }

            var appUpdateOptions = AppUpdateOptions.ImmediateAppUpdateOptions();
            StartCoroutine(StartImmediateUpdate(appUpdateInfoResult, appUpdateOptions));
        }
    }

    IEnumerator StartImmediateUpdate(AppUpdateInfo appUpdateInfoOp_i, AppUpdateOptions appUpdateOptions_i)
    {
        var startUpdateRequest = appUpdateManager.StartUpdate(appUpdateInfoOp_i, appUpdateOptions_i);
        yield return startUpdateRequest;
    }
}

