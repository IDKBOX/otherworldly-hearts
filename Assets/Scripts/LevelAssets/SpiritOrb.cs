using UnityEngine;

public class SpiritOrb : MonoBehaviour
{
    public SpiritGate linkedSpiritGate;
    public AudioClip SFXOrbCollect;
    private ParticleSystem collectEffect;
    private Animator animator;
    public GameObject audioSource;

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
            Destroy(audioSource);

            //destroy when touched
            animator.SetTrigger("Collected");
            SoundManager.Instance.PlaySound(SFXOrbCollect);
            CinemachineShake.Instance.ShakeCamera(2f, 0.1f);
            Destroy(GetComponentInChildren<SpriteRenderer>().gameObject, 0.5f);
            Destroy(GetComponent<CircleCollider2D>());
            Destroy(gameObject, 3f);
        }
    }
}
