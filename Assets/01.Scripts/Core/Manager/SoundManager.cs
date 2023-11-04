using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] private AudioClip[] _sfxSounds; //사운드 이펙트들
    [SerializeField] private AudioClip _backgroundSound; //배경 음악

    Dictionary<string, AudioClip> _audioClipsDisc; //사운드 이펙드들을 딕셔너리로 관리
    AudioSource _bgmPlayer; //배경 음악 플레이어
    AudioSource _sfxPlayer; //사운드 이펙트 플레이어

    protected override void Awake()
    {
        base.Awake();
        _bgmPlayer = transform.Find("BGMPlayer").GetComponent<AudioSource>();
        _sfxPlayer = transform.Find("SFXPlayer").GetComponent<AudioSource>();

        _audioClipsDisc = new Dictionary<string, AudioClip>();
        foreach (AudioClip clip in _sfxSounds)
        {
            _audioClipsDisc.Add(clip.name, clip); //배열에 있는 사운드 이펙트들을 딕셔너리에 모두 추가해줌
        }

        PlayBGMSound();
    }

    public void PlaySFXSound(string clip_name) //사운드 이펙트 재생 시 딕셔너리에 있는 클립의 이름으로 특정하여 재생
    {
        if (!_audioClipsDisc.ContainsKey(clip_name))
        {
            Debug.Log($"{clip_name} is not Contained at audioClipsDisc");
            return;
        }

        _sfxPlayer.PlayOneShot(_audioClipsDisc[clip_name]);
    }

    public void PlayBGMSound()
    {
        _bgmPlayer.clip = _backgroundSound;
        _bgmPlayer.Play();
    }

    public void SetVolumeSFX(float volume)
    {
        _sfxPlayer.volume = volume;
    }

    public void SetVolumeBGM(float volume)
    {
        _bgmPlayer.volume = volume;
    }
}
