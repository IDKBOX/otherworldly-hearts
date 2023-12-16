using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioClip SFXHover;
    public AudioClip SFXClick;

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySound(SFXHover);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySound(SFXClick);
    }
}
