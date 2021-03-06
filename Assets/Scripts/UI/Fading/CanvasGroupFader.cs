using UnityEngine.UI;
using UnityEngine;
using System;
using System.Collections;

//A simple class that does fading. It might appear more complicated than it needs to be at first, which you don't have to worry about. You may simply ignore all that if you wish.
//I've added comments in case you want to look into the reasoning behind this.

[RequireComponent(typeof(CanvasGroup))]
public class CanvasGroupFader : MonoBehaviour
{
    [SerializeField] bool fadeInOnSceneStart = true;
    [SerializeField] float initialWaitTime = 1f;
    [SerializeField] float fadeInSpeed = 1f;
    [SerializeField] float fadeOutSpeed = 1f;
    CanvasGroup cvsGrp;

    //The Action callback is just to help with other classes being cleaner, as it is common to fadeIn/fadeOut and then do something else. 
    Action fadeInFinishedCallback;
    Action fadeOutFinishedCallback;


    #region Monobehavior
    void Awake ()
    {
        //Reference
        cvsGrp = GetComponent<CanvasGroup>();
    }

    IEnumerator Start()
    {
        if (fadeInOnSceneStart)
        {
            InstantOpaque();
            yield return new WaitForSeconds(initialWaitTime);
            FadeOut();
        }
    }
    #endregion

    #region Public - Fade Calls
    public void FadeIn(Action callBack = null)
    {
        fadeInFinishedCallback = callBack;
        StartCoroutine(DoFadeIn());
    }

    public void FadeOut(Action callBack = null)
    {
        fadeOutFinishedCallback = callBack;
        StartCoroutine(DoFadeOut());
    }

    public void InstantOpaque()
    {
        UIFadeUtil.Canvas_InstantOpaque(cvsGrp);
    }

    public void InstantTransparent()
    {
        UIFadeUtil.Canvas_InstantOpaque(cvsGrp);
    }
    #endregion

    #region Fading
    IEnumerator DoFadeIn()
    {
        yield return StartCoroutine(UIFadeUtil.Canvas_FadeToOpaque(cvsGrp, fadeInSpeed));

        if (fadeInFinishedCallback != null)
            fadeInFinishedCallback();
    }

    IEnumerator DoFadeOut()
    {
        yield return StartCoroutine(UIFadeUtil.Canvas_FadeToTransparent(cvsGrp, fadeInSpeed));

        if (fadeOutFinishedCallback != null)
            fadeOutFinishedCallback();
    }
    #endregion
}


/* This is a previous version for your reference. I didn't adopt this because it's a little complicated even for me, as I'm not a real programmer lol. Instead, I wrote a super simple version above
 * 
 * [RequireComponent(typeof(CanvasGroup))]
public class CanvasGroupFader : MonoBehaviour
{
    [SerializeField] bool fadeInOnSceneStart = true;
    [SerializeField] float initialWaitTime = 1f;
    [SerializeField] float fadeInSpeed = 1f;
    [SerializeField] float fadeOutSpeed = 1f;
    CanvasGroup cvsGrp;
    
    IEnumerator fadeInCoroutine;
    IEnumerator fadeOutCoroutine;

    Action fadeInFinishedCallback;
    Action fadeOutFinishedCallback;


    #region Mono
    void Awake ()
    {
        cvsGrp = GetComponent<CanvasGroup>();
    }

    IEnumerator Start()
    {
        RefreshCoroutineReference();

        if (fadeInOnSceneStart)
        {
            InstantOpaque();
            yield return new WaitForSeconds(initialWaitTime);
            FadeOut();
        }
    }
    #endregion


    #region Public - Fade Calls
    public void FadeIn(Action callBack = null)
    {
        fadeInFinishedCallback = callBack;
        StartCoroutine(fadeInCoroutine);
    }

    public void FadeOut(Action callBack = null)
    {
        fadeOutFinishedCallback = callBack;
        StartCoroutine(fadeOutCoroutine);
    }


    public void InstantOpaque()
    {
        UIFadeUtil.Canvas_InstantOpaque(cvsGrp);
    }


    public void InstantTransparent()
    {
        UIFadeUtil.Canvas_InstantOpaque(cvsGrp);
    }
    #endregion

    #region Fading

    void RefreshCoroutineReference()
    {
        if (fadeInCoroutine == null)
        {
            fadeInCoroutine = UIFadeUtil.Canvas_FadeToOpaque(cvsGrp, fadeInSpeed);
        }
        if (fadeOutCoroutine == null)
        {
            fadeOutCoroutine = UIFadeUtil.Canvas_FadeToTransparent(cvsGrp, fadeInSpeed);
        }
    }

    IEnumerator DoFadeIn()
    {
        StopCoroutine(fadeInCoroutine);
        StopCoroutine(fadeOutCoroutine);
        RefreshCoroutineReference();

        yield return StartCoroutine(fadeInCoroutine);

        if (fadeInFinishedCallback != null)
            fadeInFinishedCallback();
    }

    IEnumerator DoFadeOut()
    {
        StopCoroutine(fadeInCoroutine);
        StopCoroutine(fadeOutCoroutine);
        RefreshCoroutineReference();

        yield return StartCoroutine(fadeOutCoroutine);

        if (fadeOutFinishedCallback != null)
            fadeOutFinishedCallback();
    }
    #endregion
 
     
     
     
     */
