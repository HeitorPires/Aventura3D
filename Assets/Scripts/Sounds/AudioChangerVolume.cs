using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioChangerVolume : MonoBehaviour
{
    public AudioMixer group;
    public string flaotParam = "MyExposedParam";

    public void ChangeValue(float f)
    {
        group.SetFloat(flaotParam, f);
    }
}