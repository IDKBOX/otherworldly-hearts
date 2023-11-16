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
    public Animator melatiAnimator;

    public void CollectItem()
    {
        StartCoroutine(ItemCollected());
        PlayerPrefs.SetInt("StoryItemData", 0);
    }

    public IEnumerator ItemCollected()
    {
        ItemFoundUI.SetActive(true);
        melatiAnimator.SetTrigger("Out");
        yield return new WaitForSecondsRealtime(0.15f);
        FindAnyObjectByType<CharacterMovement>().ghostCompanionUnlocked = true;
        FindAnyObjectByType<CharacterMovement>().doubleJumpUnlocked = true;
        Time.timeScale = 0.25f;
        yield return new WaitForSecondsRealtime(0.1f);
        FindAnyObjectByType<GhostFollow>().gameObject.transform.position = transform.position;
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
