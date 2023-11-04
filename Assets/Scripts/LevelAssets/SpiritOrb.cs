using UnityEngine;

public class SpiritOrb : MonoBehaviour
{
    public SpiritGate linkedSpiritGate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            linkedSpiritGate.spiritOrbsRequired--;
            Destroy(gameObject);
        }
    }
}
