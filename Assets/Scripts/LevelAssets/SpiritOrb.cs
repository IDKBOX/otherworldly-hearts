using UnityEngine;

public class SpiritOrb : MonoBehaviour
{
    public SpiritGate linkedSpiritGate;
    private ParticleSystem collectEffect;

    private void Awake()
    {
        collectEffect = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            linkedSpiritGate.spiritOrbsRequired--;
            collectEffect.Play();

            //destroy when touched
            Destroy(GetComponentInChildren<SpriteRenderer>().gameObject);
            Destroy(GetComponent<CircleCollider2D>());
            Destroy(gameObject, 3f);
        }
    }
}
