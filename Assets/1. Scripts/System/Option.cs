using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using TMPro;

public class Option : MonoBehaviour
{
    [SerializeField] private Button showOptionButton;
    [SerializeField] private Button closeOptionButton;

    [SerializeField] private GameObject blurPanel;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private Button musicButton;
    [SerializeField] private Image musicBGImage;
    [SerializeField] private Image musicOnOffImage;
    [SerializeField] private Image musicTextImage;
    [SerializeField] private Sprite[] musicBGSprites;
    [SerializeField] private Sprite[] musicOnOffSprites;
    [SerializeField] private Sprite[] musicTextSprites;
    private bool isMusicOn = true;

    [TitleGroup("Exit")] 
    [SerializeField] private TextMeshProUGUI exitText;
    private float pressedTime;
    private float exitDelay = 2f;

    // Start is called before the first frame update
    void Start()
    {
        blurPanel.SetActive(false);

        isMusicOn = PlayerPrefs.GetInt("MusicState", 1) == 1;
        musicSource.mute = !isMusicOn;
        UpdateMusicUI();

        showOptionButton.onClick.AddListener(ShowOption);
        closeOptionButton.onClick.AddListener(CloseOption);
        musicButton.onClick.AddListener(ToggleMusic);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.time - pressedTime < exitDelay)
            {
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
            else
            {
                pressedTime = Time.time;
                ShowExitWarning();
            }
        }
    }

    private void ShowOption()
    {
        blurPanel.SetActive(true);
    }

    private void CloseOption()
    {
        blurPanel.SetActive(false);
    }

    private void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        musicSource.mute = !isMusicOn;
        PlayerPrefs.SetInt("MusicState", isMusicOn ? 1 : 0);
        PlayerPrefs.Save();
        UpdateMusicUI();
    }

    private void UpdateMusicUI()
    {
        musicBGImage.sprite = isMusicOn ? musicBGSprites[1] : musicBGSprites[0];
        musicOnOffImage.sprite = isMusicOn ? musicOnOffSprites[1] : musicOnOffSprites[0];
        musicTextImage.sprite = isMusicOn ? musicTextSprites[1] : musicTextSprites[0];

        RectTransform rt = musicOnOffImage.GetComponent<RectTransform>();
        rt.anchoredPosition = isMusicOn ? new Vector2(75, 0) : new Vector2(-75, 0);
    }

    private void ShowExitWarning()
    {
        exitText.gameObject.SetActive(true);
        exitText.text = "한 번 더 누르면 종료됩니다.";
        Invoke("HideExitWarning", exitDelay);
    }

    private void HideExitWarning()
    {
        exitText.gameObject.SetActive(false);
    }
}
