using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonOnClick : MonoBehaviour, IPointerClickHandler
{
    MainMenuEffects menuEffects;
    bool canPlay;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (canPlay)
            menuEffects.Play_UI_Click();
    }

    IEnumerator Start()
    {
        menuEffects = MainMenuEffects.Instance;
        yield return new WaitForSeconds(2f);
        canPlay = true;
    }
}
