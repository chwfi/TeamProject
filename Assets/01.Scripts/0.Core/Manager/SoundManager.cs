using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] private AudioClip[] _sfxSounds; //사운드 이펙트들
    [SerializeField] private AudioClip _backgroundSound; //배경 음악

    Dictionary<string, AudioClip> _audioClipsDisc; //사운드 이펙드들을 딕셔너리로 관리
    Dictionary<AudioClip, AudioSource> _audioClipToAudioSourceDisc; //사운드 이펙드들을 딕셔너리로 관리
    AudioSource _bgmPlayer; //배경 음악 플레이어
    //AudioSource _sfxPlayer; //사운드 이펙트 플레이어

    protected override void Awake()
    {
        base.Awake();
        _bgmPlayer = transform.Find("BGMPlayer").GetComponent<AudioSource>();
        //_sfxPlayer = transform.Find("SFXPlayer").GetComponent<AudioSource>();

        _audioClipsDisc = new Dictionary<string, AudioClip>();
        _audioClipToAudioSourceDisc = new();

        foreach (AudioClip clip in _sfxSounds)
        {
            GameObject audioSrObj = new GameObject(clip.name);
            audioSrObj.transform.parent = transform.Find("SFXCollection");
            AudioSource audioSr = audioSrObj.AddComponent<AudioSource>();

            audioSr.clip = clip;

            _audioClipsDisc.Add(clip.name, clip); //배열에 있는 사운드 이펙트들을 딕셔너리에 모두 추가해줌
            _audioClipToAudioSourceDisc.Add(clip, audioSr);
        }
    }
    private void Start()
    {
        PlayBGMSound();
    }

    public void PlaySFXSound(SFX sfx) //사운드 이펙트 재생 시 딕셔너리에 있는 클립의 이름으로 특정하여 재생
    {
        if (!_audioClipsDisc.ContainsKey(sfx.ToString()))
        {
            Debug.Log($"{sfx.ToString()} is not Contained at audioClipsDisc");
            return;
        }

        var clip = _audioClipsDisc[sfx.ToString()];
        _audioClipToAudioSourceDisc[clip].Play();
    }

    public void PauseSFXSound(SFX sfx)
    {
        var clip = _audioClipsDisc[sfx.ToString()];
        var audio = _audioClipToAudioSourceDisc[clip];
        StartCoroutine(FadeOut(audio));
    }

    IEnumerator FadeOut(AudioSource audio)
    {
        float startVolume = audio.volume; // 오디오 시작 볼륨 저장

        while (audio.volume > 0)
        {
            audio.volume -= startVolume * Time.deltaTime / .4f; // 시간에 따라 볼륨을 서서히 감소시킴
            yield return null;
        }

        audio.Stop(); // 오디오를 정지시킴
        audio.volume = startVolume; // 시작 볼륨으로 초기화
    }

    public void PlayBGMSound()
    {
        _bgmPlayer.clip = _backgroundSound;
        _bgmPlayer.Play();
    }

    public void SetVolumeSFX(float volume)
    {
        //_sfxPlayer.volume = volume;

        foreach (var item in _audioClipToAudioSourceDisc)
        {
            var sr = item.Value;
            sr.volume = volume;
        }
    }

    public void SetVolumeBGM(float volume)
    {
        _bgmPlayer.volume = volume;
    }

    public AudioSource GetAudioSource(string clip_name)
    {
        if (!_audioClipsDisc.ContainsKey(clip_name))
        {
            Debug.Log($"{clip_name} is not Contained at audioClipsDisc");
            return null;
        }

        var clip = _audioClipsDisc[clip_name];
        return _audioClipToAudioSourceDisc[clip];
    }
}
