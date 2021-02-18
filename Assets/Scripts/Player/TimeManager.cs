using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowDownFactor = 0.5f;
    public float slowConstantDuration = 0.5f;
    public float rampUpDuration = 2;

    private void Update()
    {
        if (Input.GetKeyDown("q") && !InSlowMo)
        {
            StartCoroutine(EnterSlowMo());
        }
    }

    //void OnGUI()
    //{
    //    GUI.Label(new Rect(20, 20, 500, 20), "Time.unscaledDeltaTime: " + Time.unscaledDeltaTime);
    //    GUI.Label(new Rect(20, 40, 500, 20), "Time.timeScale: " + Time.timeScale);
    //}

    bool InSlowMo;
    IEnumerator EnterSlowMo()
    {
        InSlowMo = true;

        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        yield return new WaitForSeconds(slowConstantDuration);

        while (Time.timeScale < 1f)
        {
            yield return null;

            Time.timeScale += (1f / rampUpDuration * Time.unscaledDeltaTime);
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
        Time.timeScale = 1f;
        InSlowMo = false;
    }
}


/*
     void ToggleSlow ()
    {
        Time.timeScale = (slowmo = !slowmo) ? slowDownFactor : 1f;


        Time.fixedDeltaTime = Time.timeScale * 0.02f;

        Time.timeScale += (1f / slowDownLength * Time.unscaledDeltaTime);
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }

 */