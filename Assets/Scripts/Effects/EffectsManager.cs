using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Core.Singleton;

public class EffectsManager : Singleton<EffectsManager>
{
    public PostProcessVolume processVolume;
    public float duration = 1f;
    public float vignetteIntensity = .5f;

    [SerializeField] private Vignette _vignette;

    private void Start()
    {
        GetVignette();
    }

    private void GetVignette()
    {
        processVolume.profile.TryGetSettings<Vignette>(out _vignette);
    }


    [NaughtyAttributes.Button]
    public void ChangeVignette()
    {
        StartCoroutine(ChangeVignetteColorCoroutine(Color.red, Color.black));
    }

    IEnumerator ChangeVignetteColorCoroutine(Color color1, Color color2)
    {
        yield return StartCoroutine(FlashVignneteColorAndIntensity(color1, vignetteIntensity));
        yield return StartCoroutine(FlashVignneteColorAndIntensity(color2, 0));
    }

    IEnumerator FlashVignneteColorAndIntensity(Color targetColor, float targetIntensity)
    {
        ColorParameter c = new ColorParameter();
        FloatParameter intensity = new FloatParameter();

        float time = 0;
        float initialIntensity = _vignette.intensity.value;
        Color initialColor = _vignette.color.value;


        while (time < duration)
        {

            c.value = Color.Lerp(initialColor, targetColor, time / duration);
            _vignette.color.Override(c);


            intensity.value = Mathf.Lerp(initialIntensity, targetIntensity, time / duration);
            _vignette.intensity.Override(intensity);

            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }


        c.value = targetColor;
        _vignette.color.Override(c);

        intensity.value = targetIntensity;
        _vignette.intensity.Override(intensity);
    }




}
