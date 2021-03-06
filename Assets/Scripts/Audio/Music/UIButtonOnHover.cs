using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonOnHover : MonoBehaviour, IPointerEnterHandler
{
    MainMenuEffects menuEffects;
    public void OnPointerEnter(PointerEventData eventData)
    {
        menuEffects.Play_UI_Hover();
    }

    void Start()
    {
        menuEffects = MainMenuEffects.Instance;
    }
}
