using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.InputSystem.Layouts;

public class CustomOnScreenButton : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public bool holdOnExit;
    [HideInInspector] public bool BlockEnter;
    [HideInInspector] public bool isDown;

    public void OnPointerUp(PointerEventData eventData)
    {
        SendValueToControl(0.0f);
        isDown = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SendValueToControl(1.0f);
        isDown = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!BlockEnter)
        {
            SendValueToControl(1.0f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!holdOnExit)
        {
            SendValueToControl(0.0f);
        }
    }

    public void Release()
    {
        SendValueToControl(0.0f);
    }

    [InputControl(layout = "Button")]
    [SerializeField]
    private string m_ControlPath;

    protected override string controlPathInternal
    {
        get => m_ControlPath;
        set => m_ControlPath = value;
    }
}
