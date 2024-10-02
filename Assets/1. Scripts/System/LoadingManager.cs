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

    [SerializeField] private Image loadingBar;

    private void Start()
    {
        data = DataManager.Instance;
        GameManager.Instance.sceneName = gameScene;
        
    }

    public void StartCoroutine()
    {
        StartCoroutine(LoadScene());
    }
    private IEnumerator LoadScene()
    {
        //if (data.CheckFile())
        //    data.LoadData();

        AsyncOperation gameLoad = SceneManager.LoadSceneAsync(gameScene);
        gameLoad.allowSceneActivation = false;

        float elapsedTime = 0f;
        while (!gameLoad.isDone)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / loadingTime);

            loadingBar.fillAmount = progress;

            if (elapsedTime >= loadingTime)
            {
                loadingBar.fillAmount = 1.0f;
                yield return new WaitForSeconds(1f);
                gameLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
