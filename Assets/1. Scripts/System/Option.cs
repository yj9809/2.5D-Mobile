using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Option : MonoBehaviour
{
    [SerializeField] private GameObject blurPanel;
    [SerializeField] private Button soundToggleButton;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Sprite soundOn;
    [SerializeField] private Sprite soundOff;
    private bool isSoundOn = true;
    private MainCamera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = FindObjectOfType<MainCamera>();

        blurPanel.SetActive(false);

        isSoundOn = PlayerPrefs.GetInt("SoundState", 1) == 1;
        audioSource.mute = !isSoundOn;
        UpdateSoundImage();
    }

    public void SoundButton()
    {
        isSoundOn = !isSoundOn;
        audioSource.mute = !isSoundOn;

        PlayerPrefs.SetInt("SoundState", isSoundOn ? 1 : 0);
        PlayerPrefs.Save();

        UpdateSoundImage();
    }

    private void UpdateSoundImage()
    {
        soundToggleButton.image.sprite = isSoundOn ? soundOn : soundOff;
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
