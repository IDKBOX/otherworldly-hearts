using System.Collections;
using UnityEngine;

public class StoryItem : MonoBehaviour
{
    public int ItemID;
    [Header("Prerequisites")]
    public GameObject ItemFoundUI;
    private ParticleSystem collectEffect;
    public Animator itemAnimator;
    public Animator UIAnimator;

    private void Awake()
    {
        collectEffect = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(ItemCollected());
        }
    }

    private IEnumerator ItemCollected()
    {
        //when first collected
        collectEffect.Play();
        itemAnimator.SetTrigger("Collected");
        Destroy(GetComponentInChildren<SpriteRenderer>().gameObject, 0.5f);
        Destroy(GetComponent<CircleCollider2D>());
        FindObjectOfType<ItemDisplay>().itemsToShow = ItemID;
        ItemFoundUI.SetActive(true);
        yield return new WaitForSecondsRealtime(0.15f);
        Time.timeScale = 0.25f;
        yield return new WaitForSecondsRealtime(4f);
        UIAnimator.SetTrigger("End");
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
        yield return new WaitForSecondsRealtime(2f);
        ItemFoundUI.SetActive(false);
        Destroy(gameObject, 0.1f);
    }

    //TODO
    //ability unlock
    //change UI text
}
