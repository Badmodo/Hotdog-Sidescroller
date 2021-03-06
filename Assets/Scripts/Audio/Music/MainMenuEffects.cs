using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MainMenuEffects : MonoBehaviour
{
    public static MainMenuEffects Instance;

    [SerializeField] float waitBeforeShutter = 0.8f;
    [SerializeField] float waitBeforeBGM = 1.5f;
    [SerializeField] float vignetteFadeSpeed = 0.2f;
    [SerializeField] CanvasGroupFader fader;
    [SerializeField] GameObject sfx_scrollingClip;
    [SerializeField] AudioSource sfx_UI_Hover;
    [SerializeField] AudioSource sfx_UI_Click;

    Vignette vignette;
    AudioSource BGM;

    public void Play_UI_Hover() => Instantiate(sfx_UI_Hover, Vector3.zero, Quaternion.identity);
    public void Play_UI_Click() => Instantiate(sfx_UI_Click, Vector3.zero, Quaternion.identity);

    private void Awake()
    {
        Instance = this;

        BGM = GetComponent<AudioSource>();

        //Ref PP
        PostProcessVolume volume = gameObject.GetComponent<PostProcessVolume>();

        //Fade out vignett
        if (volume.profile.TryGetSettings(out vignette))
        {
            Debug.Log(vignette == null);
            StartCoroutine(FadeOutVignette());
        }
    }

    IEnumerator Start()
    {
        //Audio sequence
        yield return new WaitForSeconds(waitBeforeShutter);
        Instantiate(sfx_scrollingClip, Vector3.zero, Quaternion.identity);
        yield return new WaitForSeconds(waitBeforeBGM);
        BGM.Play();
    }

    IEnumerator FadeOutVignette ()
    {
        //vignette.intensity.value = 1f;
        while (vignette.intensity.value > 0f)
        {
            vignette.intensity.value -= Time.deltaTime * vignetteFadeSpeed;
            yield return null;
        }
    }
}
