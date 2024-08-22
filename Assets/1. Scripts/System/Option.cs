using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    private MainCamera _camera;
    [SerializeField] private GameObject blurPanel;

    [SerializeField] private Button musicButton;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private Image musicOn_OffImage;
    [SerializeField] private Sprite[] musicOn_Off;
    [SerializeField] private Image musicButtonImage;
    [SerializeField] private Sprite[] musicButtonSprite;
    private bool isMusicOn = true;

    // Start is called before the first frame update
    void Start()
    {
        _camera = FindObjectOfType<MainCamera>();
        blurPanel.SetActive(false);

        isMusicOn = PlayerPrefs.GetInt("MusicState", 1) == 1;
        musicSource.mute = !isMusicOn;

        musicSlider.value = PlayerPrefs.GetFloat("MusicState", 1f);
        musicSource.volume = musicSlider.value;

        musicButton.onClick.AddListener(ToggleMusic);
        musicSlider.onValueChanged.AddListener(UpdateMusicVolume);
    }

    private void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        musicSource.mute = !isMusicOn;
        musicOn_OffImage.sprite = isMusicOn ? musicOn_Off[1] : musicOn_Off[0];
        musicButtonImage.sprite = isMusicOn ? musicButtonSprite[1] : musicButtonSprite[0];

        PlayerPrefs.SetInt("MusicState", isMusicOn ? 1 : 0);
    }
    private void UpdateMusicVolume(float value)
    {
        musicSource.volume = value; 
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();
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
