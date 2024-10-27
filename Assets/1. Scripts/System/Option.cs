using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using TMPro;

// �̰͵� �ǿ��� ��ǰ ���⼭ ���� �ǵ�� ��� ã�� ����
// ���� ����� ���ó� �����ϰ� �ǿ������� ���� ��Ź
public class Option : MonoBehaviour
{
    [SerializeField] private Button showOptionButton;
    [SerializeField] private Button closeOptionButton;

    [SerializeField] private GameObject blurPanel;

    [SerializeField] private Button musicButton;
    [SerializeField] private Button effectButton;
    [SerializeField] private Image musicBGImage;
    [SerializeField] private Image musicOnOffImage;
    [SerializeField] private Image musicTextImage;
    [SerializeField] private Image effectBGImage;
    [SerializeField] private Image effectOnOffImage;
    [SerializeField] private Image effectTextImage;
    [SerializeField] private Sprite[] musicOnOffSprites;
    [SerializeField] private Sprite[] effectOnOffSprites;
    [SerializeField] private Sprite[] audioBGSprites;
    [SerializeField] private Sprite[] audioTextSprites;
    private AudioManager audioManager;

    [TitleGroup("Exit")] 
    [SerializeField] private TextMeshProUGUI exitText;
    private float pressedTime;
    private float exitDelay = 2f;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.Instance;
        blurPanel.SetActive(false);

        showOptionButton.onClick.AddListener(ShowOption);
        closeOptionButton.onClick.AddListener(CloseOption);
        musicButton.onClick.AddListener(ToggleMusic);
        effectButton.onClick.AddListener(ToggleEffect);

        LoadUIState();
    }

    private void Update()
    {
        ExitGame();
    }

    // �ɼ� ��ư (��)Ȱ��ȭ
    public void OptionButtonActive(bool isActive)
    {
        showOptionButton.interactable = isActive;
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
        audioManager.ToggleMusic();
        UpdateMusicUI();
        SaveUIState();
    }
    private void ToggleEffect()
    {
        audioManager.ToggleEffect();
        UpdateEffectUI();
        SaveUIState();
    }

    private void UpdateMusicUI()
    {
        bool isMusicOn = audioManager.IsMusicOn();
        musicOnOffImage.sprite = isMusicOn ? musicOnOffSprites[1] : musicOnOffSprites[0];
        musicBGImage.sprite = isMusicOn ? audioBGSprites[1] : audioBGSprites[0];
        musicTextImage.sprite = isMusicOn ? audioTextSprites[1] : audioTextSprites[0];

        RectTransform rt = musicOnOffImage.GetComponent<RectTransform>();
        rt.anchoredPosition = isMusicOn ? new Vector2(75, 0) : new Vector2(-75, 0);
    }
    private void UpdateEffectUI()
    {
        bool isEffectOn = audioManager.IsEffectOn();
        effectOnOffImage.sprite = isEffectOn ? effectOnOffSprites[1] : effectOnOffSprites[0];
        effectBGImage.sprite = isEffectOn ? audioBGSprites[1] : audioBGSprites[0];
        effectTextImage.sprite = isEffectOn ? audioTextSprites[1] : audioTextSprites[0];

        RectTransform rt = effectOnOffImage.GetComponent<RectTransform>();
        rt.anchoredPosition = isEffectOn ? new Vector2(75, 0) : new Vector2(-75, 0);
    }

    private void SaveUIState()
    {
        PlayerPrefs.SetInt("MusicUIState", audioManager.IsMusicOn() ? 1 : 0);
        PlayerPrefs.SetInt("EffectUIState", audioManager.IsEffectOn() ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadUIState()
    {
        bool musicState = PlayerPrefs.GetInt("MusicUIState", 1) == 1;
        bool effectState = PlayerPrefs.GetInt("EffectUIState", 1) == 1;

        audioManager.SetMusicState(musicState);
        audioManager.SetEffectState(effectState);

        UpdateMusicUI();
        UpdateEffectUI();
    }

    private void ExitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.time - pressedTime < exitDelay)
            {
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                DataManager.Instance.GameDataUpdate();
            }
            else
            {
                pressedTime = Time.time;
                ShowExitWarning();
            }
        }
    }

    private void ShowExitWarning()
    {
        exitText.gameObject.SetActive(true);
        exitText.text = "�� �� �� ������ ����˴ϴ�.";
        Invoke("HideExitWarning", exitDelay);
    }

    private void HideExitWarning()
    {
        exitText.gameObject.SetActive(false);
    }
}
