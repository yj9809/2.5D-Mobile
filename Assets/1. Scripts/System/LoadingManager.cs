using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    private static float loadingTime = 3f;
    private static string gameScene = "Game";
    private static string uiScene = "UI";

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(loadingTime);

        AsyncOperation gameLoad = SceneManager.LoadSceneAsync(gameScene);
        AsyncOperation uiLoad = SceneManager.LoadSceneAsync(uiScene, LoadSceneMode.Additive);

        while (!gameLoad.isDone || !uiLoad.isDone)
        {
            yield return null;
        }
    }
}
