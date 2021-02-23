using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillManager : MonoBehaviour
{
    public Text kills_UI;
    public int Kills_Counts; //How many kills

    public void Increase_score() //This will update the UI text to the current kills count 
    {
        Kills_Counts++;
        kills_UI.text = Kills_Counts.ToString();
    }
}
