using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GhostFound : MonoBehaviour
{
    public SpiritGate linkedSpiritGate;
    public UnityEvent onComplete;

    [Header("Prerequisites")]
    public Animator UIAnimator;
    public GameObject ItemFoundUI;

    public void CollectItem()
    {
        StartCoroutine(ItemCollected());
    }

    public IEnumerator ItemCollected()
    {
        Destroy(GetComponentInChildren<SpriteRenderer>().gameObject, 0.5f);
        Destroy(GetComponent<CircleCollider2D>());
        ItemFoundUI.SetActive(true);
        yield return new WaitForSecondsRealtime(0.15f);
        Time.timeScale = 0.25f;
        yield return new WaitForSecondsRealtime(4f);
        UIAnimator.SetTrigger("End");
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
        onComplete?.Invoke();
        yield return new WaitForSecondsRealtime(1f);
        ItemFoundUI.SetActive(false);
    }

    public void OpenSpiritGate()
    {
        if (linkedSpiritGate != null)
        {
            linkedSpiritGate.spiritOrbsRequired--;
        }
    }
}
