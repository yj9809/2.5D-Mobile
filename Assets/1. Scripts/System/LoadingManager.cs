using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    private DataManager data;
    private static float loadingTime = 3f;
    private static string gameScene = "Game";

    private void Start()
    {
        data = DataManager.Instance;
        GameManager.Instance.sceneName = gameScene;
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(loadingTime);

        if (data.CheckFile())
            data.LoadData();

        AsyncOperation gameLoad = SceneManager.LoadSceneAsync(gameScene);

        while (!gameLoad.isDone)
        {
            yield return null;
        }
    }
}
