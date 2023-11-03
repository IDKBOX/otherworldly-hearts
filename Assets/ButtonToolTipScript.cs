using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonToolTipScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject toolTipPrefab;
    private Coroutine delay;

    public void OnPointerEnter(PointerEventData eventData)
    {
        delay = StartCoroutine(DelayTooltip());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTipPrefab.SetActive(false);
        StopCoroutine(delay);
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        toolTipPrefab.SetActive(false);
        StopCoroutine(delay);
    }

    IEnumerator DelayTooltip()
    {
        yield return new WaitForSeconds(0.1f);
        toolTipPrefab.SetActive(true);
    }
}
