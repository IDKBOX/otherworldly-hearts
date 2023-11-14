using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StoryItem : MonoBehaviour
{
    public int ItemID;
    public SpiritGate linkedSpiritGate;
    public UnityEvent onComplete;

    [Header("Prerequisites")]
    public GameObject ItemFoundUI;
    private ParticleSystem collectEffect;
    public Animator itemAnimator;
    public Animator UIAnimator;
    public Image itemImage;
    public TextMeshProUGUI itemFoundText;
    public TextMeshProUGUI descriptionText;
    public Sprite ghostSprite;
    public Sprite item1Sprite;
    public Sprite item2Sprite;
    public Sprite item3Sprite;

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

    public IEnumerator ItemCollected()
    {
        collectEffect.Play();
        itemAnimator.SetTrigger("Collected");
        Destroy(GetComponentInChildren<SpriteRenderer>().gameObject, 0.5f);
        Destroy(GetComponent<CircleCollider2D>());
        IDChecker();
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

    private void IDChecker()
    {
        ItemDisplay.itemsToShow = ItemID;

        switch (ItemID)
        {
            case 0:
                itemFoundText.text = "Ghost Found";
                descriptionText.text = "Double Jump Unlocked";
                itemImage.sprite = ghostSprite;
                FindAnyObjectByType<CharacterMovement>().ghostCompanionUnlocked = true;
                FindAnyObjectByType<CharacterMovement>().doubleJumpUnlocked = true;
                break;
            case 1:
                itemFoundText.text = "Old Photo Found";
                descriptionText.text = "Dash Unlocked";
                itemImage.sprite = item1Sprite;
                FindAnyObjectByType<CharacterMovement>().dashUnlocked = true;
                break;
            case 2:
                itemFoundText.text = "Old Diary Found";
                descriptionText.text = "???";
                itemImage.sprite = item2Sprite;
                break;
            case 3:
                itemFoundText.text = "Flower Found";
                descriptionText.text = "Story Complete!";
                itemImage.sprite = item3Sprite;
                break;
        }
    }

    public void OpenSpiritGate()
    {
        if (linkedSpiritGate != null)
        {
            linkedSpiritGate.spiritOrbsRequired--;
        }
    }
}
