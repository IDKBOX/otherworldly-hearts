using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonToolTipScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject toolTipPrefab;
    private Coroutine delay;
    public AudioClip SFXHover;
    public AudioClip SFXClick;

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySound(SFXHover);
        delay = StartCoroutine(DelayTooltip());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTipPrefab.SetActive(false);
        StopCoroutine(delay);
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySound(SFXClick);
        toolTipPrefab.SetActive(false);
        StopCoroutine(delay);
    }

    IEnumerator DelayTooltip()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        toolTipPrefab.SetActive(true);
    }
}
