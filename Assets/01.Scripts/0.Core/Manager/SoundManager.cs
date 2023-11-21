using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] private AudioClip[] _sfxSounds; //���� ����Ʈ��
    [SerializeField] private AudioClip _backgroundSound; //��� ����

    Dictionary<string, AudioClip> _audioClipsDisc; //���� �������� ��ųʸ��� ����
    Dictionary<AudioClip, AudioSource> _audioClipToAudioSourceDisc; //���� �������� ��ųʸ��� ����
    AudioSource _bgmPlayer; //��� ���� �÷��̾�
    //AudioSource _sfxPlayer; //���� ����Ʈ �÷��̾�

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

            _audioClipsDisc.Add(clip.name, clip); //�迭�� �ִ� ���� ����Ʈ���� ��ųʸ��� ��� �߰�����
            _audioClipToAudioSourceDisc.Add(clip, audioSr);
        }
    }
    private void Start()
    {
        PlayBGMSound();
    }

    public void PlaySFXSound(SFX sfx) //���� ����Ʈ ��� �� ��ųʸ��� �ִ� Ŭ���� �̸����� Ư���Ͽ� ���
    {
        if (!_audioClipsDisc.ContainsKey(sfx.ToString()))
        {
            Debug.Log($"{sfx.ToString()} is not Contained at audioClipsDisc");
            return;
        }

        //_sfxPlayer.PlayOneShot(_audioClipsDisc[clip_name]);
        var clip = _audioClipsDisc[sfx.ToString()];
        _audioClipToAudioSourceDisc[clip].Play();
    }

    public void PauseSFXSound(SFX sfx)
    {
        var clip = _audioClipsDisc[sfx.ToString()];
        _audioClipToAudioSourceDisc[clip].Stop();

        //_sfxPlayer.Stop();
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
