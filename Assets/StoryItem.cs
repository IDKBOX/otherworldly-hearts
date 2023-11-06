using System.Collections;
using UnityEngine;

public class StoryItem : MonoBehaviour
{
    public int ItemID;
    public GameObject ItemFoundUI;
    private ParticleSystem collectEffect;
    private Animator animator;

    private void Awake()
    {
        collectEffect = GetComponentInChildren<ParticleSystem>();
        animator = GetComponentInChildren<Animator>();
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
        collectEffect.Play();
        animator.SetTrigger("Collected");
        Destroy(GetComponentInChildren<SpriteRenderer>().gameObject, 0.5f);
        Destroy(GetComponent<CircleCollider2D>());

        FindObjectOfType<ItemDisplay>().itemsToShow = ItemID;
        ItemFoundUI.SetActive(true);
        Time.timeScale = 0.15f;
        yield return new WaitForSecondsRealtime(4f);
        Time.timeScale = 1f;
        ItemFoundUI.SetActive(false);
        Destroy(gameObject, 0.1f);
    }
}
