using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class Mute : MonoBehaviour
{
    public AudioMixerGroup mixerGroup;
    private List<AudioSource> sources;
    public bool mute;

    // Start is called before the first frame update
    void Start()
    {
        sources = FindObjectsOfType<AudioSource>().ToList();
    }

    public void OnClick()
    {
        sources.ForEach(i => i.mute = mute);
    }
}
