using Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Audio;

public class SFXPool : Singleton<SFXPool>
{

    private List<AudioSource> _audioSourceList;
    public int poolSize = 10;
    private int _index = 0;
    public AudioMixerGroup mixerGroup;

    protected override void Awake()
    {
        base.Awake();
        CreatePool();
    }

    private void CreatePool()
    {
        _audioSourceList = new List<AudioSource>();
        for (int i = 0; i < poolSize; i++)
        {
            CreateAudioSource();
        }
    }

    private void CreateAudioSource()
    {
        GameObject go = new GameObject("SFX_Pool");
        go.transform.SetParent(transform);
        go.AddComponent<AudioSource>();
        go.GetComponent<AudioSource>().outputAudioMixerGroup = mixerGroup;
        _audioSourceList.Add(go.GetComponent<AudioSource>());
    }

    public void Play(SFXType type)
    {
        if (type == SFXType.NONE) return;
        var sfx = SoundManager.Instance.GetSFXByType(type);
        _audioSourceList[_index].clip = sfx.audioClip;
        _audioSourceList[_index].Play();
        _index++;
        if (_index >= poolSize)
        {
            _index = 0;
        }
    }

}
