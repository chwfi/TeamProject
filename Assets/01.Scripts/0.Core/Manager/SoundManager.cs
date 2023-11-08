using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] private AudioClip[] _sfxSounds; //���� ����Ʈ��
    [SerializeField] private AudioClip _backgroundSound; //��� ����

    Dictionary<string, AudioClip> _audioClipsDisc; //���� �������� ��ųʸ��� ����
    AudioSource _bgmPlayer; //��� ���� �÷��̾�
    AudioSource _sfxPlayer; //���� ����Ʈ �÷��̾�

    protected override void Awake()
    {
        base.Awake();
        _bgmPlayer = transform.Find("BGMPlayer").GetComponent<AudioSource>();
        _sfxPlayer = transform.Find("SFXPlayer").GetComponent<AudioSource>();

        _audioClipsDisc = new Dictionary<string, AudioClip>();
        foreach (AudioClip clip in _sfxSounds)
        {
            _audioClipsDisc.Add(clip.name, clip); //�迭�� �ִ� ���� ����Ʈ���� ��ųʸ��� ��� �߰�����
        }

        PlayBGMSound();
    }

    public void PlaySFXSound(string clip_name) //���� ����Ʈ ��� �� ��ųʸ��� �ִ� Ŭ���� �̸����� Ư���Ͽ� ���
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
