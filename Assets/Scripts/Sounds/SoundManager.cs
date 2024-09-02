using Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public List<MusicSetup> musicSetupList;
    public List<SFXSetup> sFXSetupsList;

    public AudioSource musicSource;

    public void PlayMusicByType(MusicType type)
    {
        var music = GetMusicByType(type);
        musicSource.clip = music.audioClip;
        musicSource.Play();

    }

    public MusicSetup GetMusicByType(MusicType type)
    {
        return musicSetupList.Find(i => i.musicType == type);
    }
    
    public SFXSetup GetSFXByType(SFXType type)
    {
        return sFXSetupsList.Find(i => i.sFXType == type);
    }

}

public enum MusicType
{
    TYPE_01,
    TYPE_02,
    TYPE_03
}

[System.Serializable]
public class MusicSetup
{
    public MusicType musicType;
    public AudioClip audioClip;
}

public enum SFXType
{
    TYPE_01,
    TYPE_02,
    TYPE_03
}

[System.Serializable]
public class SFXSetup
{
    public SFXType sFXType;
    public AudioClip audioClip;
}