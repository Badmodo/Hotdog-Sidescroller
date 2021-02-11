using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour
{
    const int MaxHealth = 3;

    [SerializeField] Image[] HealthSlots;
    [SerializeField] Sprite HealthIcon;
    public void SetHealth(int healthPoints)
    {
        for (int i = 0; i < HealthSlots.Length; i++)
        {
            if (HealthSlots[i] != null)
            {
                HealthSlots[i].enabled = i < healthPoints;
            }
        }
    }

    void Start()
    {
        if (HealthSlots .Length < MaxHealth)
        {
            Debug.LogError("Please set the correct number of health slots");
            HealthSlots = new Image[3];
        }
    }

    void ResetHealth()
    {
        SetHealth(MaxHealth);
    }
}