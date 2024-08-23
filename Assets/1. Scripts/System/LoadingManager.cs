using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    private static float loadingTime = 3f;
    private static string gameScene = "Game";

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(loadingTime);

        AsyncOperation gameLoad = SceneManager.LoadSceneAsync(gameScene);

        while (!gameLoad.isDone)
        {
            yield return null;
        }
    }
}
