using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Postie : MonoBehaviour
{
    private PostProcessVolume vol;

    private LensDistortion dist;
    private ChromaticAberration chrom;
    private ColorGrading grad;
    private Bloom bloom;
    private Vignette vig;

    private bool cambio = true;

    private void Start()
    {
        vol = GetComponent<PostProcessVolume>();
        
        vol.profile.TryGetSettings(out dist);
        vol.profile.TryGetSettings(out chrom);
        vol.profile.TryGetSettings(out grad);
        vol.profile.TryGetSettings(out bloom);
        vol.profile.TryGetSettings(out vig);

        StartCoroutine(Waiter());
    }

    private void Update()
    {
        if(cambio)
        {
            dist.intensity.value += 10 * Time.deltaTime;
            chrom.intensity.value -= 2f * Time.deltaTime;
            grad.saturation.value += 20 * Time.deltaTime;
            grad.contrast.value -= 20 * Time.deltaTime;
            bloom.intensity.value -= 10 * Time.deltaTime;
            vig.intensity.value -= .1f * Time.deltaTime;
        }
        else
        {
            dist.intensity.value -= 10 * Time.deltaTime;
            chrom.intensity.value += 2f * Time.deltaTime;
            grad.saturation.value -= 20 * Time.deltaTime;
            grad.contrast.value += 20 * Time.deltaTime;
            bloom.intensity.value += 10 * Time.deltaTime;
            vig.intensity.value += .1f * Time.deltaTime;
        }


    }
    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(5);
        cambio = !cambio;
        StartCoroutine(Waiter());
    }
}
