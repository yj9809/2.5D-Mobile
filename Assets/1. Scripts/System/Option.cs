using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    private MainCamera _camera;
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

    // Start is called before the first frame update
    void Start()
    {
        _camera = FindObjectOfType<MainCamera>();
        blurPanel.SetActive(false);

        isMusicOn = PlayerPrefs.GetInt("MusicState", 1) == 1;
        musicSource.mute = !isMusicOn;
        UpdateMusicUI();

        musicButton.onClick.AddListener(ToggleMusic);
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

    public void Zoom()
    {
        _camera.ZoomScreen();
    }

    public void ShowOption()
    {
        blurPanel.SetActive(true);
    }

    public void CloseOption()
    {
        blurPanel.SetActive(false);
    }
}
