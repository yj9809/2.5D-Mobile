using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Option : MonoBehaviour
{
    [SerializeField] private GameObject blurPanel;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button soundEffectButton;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundEffectSource;
    [SerializeField] private Sprite[] musicOn_Off;
    [SerializeField] private Sprite[] soundEffectOn_Off;
    private bool isMusicOn = true;
    private bool isSoundOn = true;
    private MainCamera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = FindObjectOfType<MainCamera>();

        blurPanel.SetActive(false);

        musicButton.onClick.AddListener(MusicButton);
        soundEffectButton.onClick.AddListener(SoundButton);

        isMusicOn = PlayerPrefs.GetInt("SoundState", 1) == 1;
        isSoundOn = PlayerPrefs.GetInt("SoundState", 1) == 1;
        musicSource.mute = !isMusicOn;
        soundEffectSource.mute = !isSoundOn;
        UpdateMusicImage();
        UpdateEffectImage();
    }

    public void MusicButton()
    {
        isMusicOn = !isMusicOn;
        musicSource.mute = !isMusicOn;

        PlayerPrefs.SetInt("SoundState", isMusicOn ? 1 : 0);
        PlayerPrefs.Save();

        UpdateMusicImage();
    }
    public void SoundButton()
    {
        isSoundOn = !isSoundOn;
        soundEffectSource.mute = !isSoundOn;

        PlayerPrefs.SetInt("SoundState", isSoundOn ? 1 : 0);
        PlayerPrefs.Save();

        UpdateEffectImage();
    }

    private void UpdateMusicImage()
    {
        musicButton.image.sprite = isMusicOn ? musicOn_Off[0] : musicOn_Off[1];
    }

    private void UpdateEffectImage()
    {
        soundEffectButton.image.sprite = isSoundOn ? soundEffectOn_Off[0] : soundEffectOn_Off[1];
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

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
