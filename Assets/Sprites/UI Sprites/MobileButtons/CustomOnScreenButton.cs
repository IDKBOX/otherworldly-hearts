using UnityEngine;
using UnityEngine.EventSystems;

public class CustomOnScreenButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public string buttonName;

    [Header("Prerequisites")]
    public CharacterMovement characterMovement;

    [HideInInspector] public bool holdOnExit;
    [HideInInspector] public bool BlockEnter;
    [HideInInspector] public bool isDown;

    public void OnPointerUp(PointerEventData eventData)
    {

        isDown = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("down");

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!BlockEnter)
        {
            
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!holdOnExit)
        {
            
        }
    }

    public void Release()
    {
        
    }
}
