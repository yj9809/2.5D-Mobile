using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 권오석 작품 내꺼 아님 추가 설명 필요한 경우
// 권오석한테 문의 바람
public class LoadingManager : MonoBehaviour
{
    private static float loadingTime = 3f;
    private static string gameScene = "Game";

    [SerializeField] private Image loadingBar;

    private void Start()
    {
        GameManager.Instance.sceneName = gameScene;
        
    }

    public void StartCoroutine()
    {
        StartCoroutine(LoadScene());
    }
    private IEnumerator LoadScene()
    {
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
