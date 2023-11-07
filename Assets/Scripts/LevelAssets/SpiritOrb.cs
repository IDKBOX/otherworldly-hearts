using UnityEngine;

public class SpiritOrb : MonoBehaviour
{
    public SpiritGate linkedSpiritGate;
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
            linkedSpiritGate.spiritOrbsRequired--;
            collectEffect.Play();

            //destroy when touched
            animator.SetTrigger("Collected");
            Destroy(GetComponentInChildren<SpriteRenderer>().gameObject, 0.5f);
            Destroy(GetComponent<CircleCollider2D>());
            Destroy(gameObject, 3f);
        }
    }
}
