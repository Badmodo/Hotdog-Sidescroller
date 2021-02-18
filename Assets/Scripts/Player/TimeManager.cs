using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowDownFactor = 0.5f;
    public float slowDownLength = 2;

    private void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            Time.timeScale = slowDownFactor;

            Time.fixedDeltaTime = Time.timeScale * 0.02f;

            Time.timeScale += (1f / slowDownLength * Time.unscaledDeltaTime);
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

        }
    }
}
