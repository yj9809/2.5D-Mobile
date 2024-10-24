using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Sirenix.OdinInspector;

public enum EffectType
{
    Store,
    Upgrade,
    UnLock
}

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioClip musicClip;
    [SerializeField] private AudioClip[] effectClips;
    private AudioSource musicSource;
    private AudioSource[] effectSources;
    private float[] effectVolumes;
    [ProgressBar(0, 2), SerializeField] private float[] defaultEffectVolumes = { 1.5f, 0.7f, 1.0f };

    private bool isMusicOn = true;
    private bool isEffectOn = true;

    private void Start()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Music")[0];
        musicSource.clip = musicClip;
        musicSource.loop = true;
        musicSource.Play();

        effectSources = new AudioSource[effectClips.Length];
        effectVolumes = new float[effectClips.Length];
        for (int i = 0; i < effectClips.Length; i++)
        {
            effectSources[i] = gameObject.AddComponent<AudioSource>();
            effectSources[i].outputAudioMixerGroup = audioMixer.FindMatchingGroups("Effect")[0];
            effectSources[i].clip = effectClips[i];
            effectVolumes[i] = defaultEffectVolumes[i];
        }

        isMusicOn = PlayerPrefs.GetInt("MusicState", 1) == 1;
        isEffectOn = PlayerPrefs.GetInt("EffectState", 1) == 1;
        UpdateMusicVolume();
        UpdateEffectVolume();
    }

    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        UpdateMusicVolume();
        PlayerPrefs.SetInt("MusicState", isMusicOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void UpdateMusicVolume()
    {
        audioMixer.SetFloat("Music", isMusicOn ? 0 : -80);
    }

    public void SetMusicState(bool state)
    {
        isMusicOn = state;
        UpdateMusicVolume();
    }

    public bool IsMusicOn() => isMusicOn;

    public void PlayEffect(EffectType effectType)
    {
        int index = (int)effectType;
        if (isEffectOn && index >= 0 && index < effectSources.Length)
        {
            effectSources[index].volume = effectVolumes[index]; // 타입별 음량 설정
            effectSources[index].Play();
        }
    }

    public void ToggleEffect()
    {
        isEffectOn = !isEffectOn;
        UpdateEffectVolume();
        PlayerPrefs.SetInt("EffectState", isEffectOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void UpdateEffectVolume()
    {
        audioMixer.SetFloat("Effect", isEffectOn ? 0 : -80);
    }

    public void SetEffectState(bool state)
    {
        isEffectOn = state;
        UpdateEffectVolume();
    }

    public bool IsEffectOn() => isEffectOn;

    // 효과음 타입별 음량 설정 메서드
    public void SetEffectVolume(EffectType effectType, float volume)
    {
        int index = (int)effectType;
        if (index >= 0 && index < effectVolumes.Length)
        {
            effectVolumes[index] = volume;
            effectSources[index].volume = volume; // 현재 효과음 소스의 음량 설정
        }
    }

    // 효과음 타입별 음량 가져오기
    public float GetEffectVolume(EffectType effectType)
    {
        int index = (int)effectType;
        if (index >= 0 && index < effectVolumes.Length)
        {
            return effectVolumes[index];
        }
        return 0; // 기본값
    }
}
