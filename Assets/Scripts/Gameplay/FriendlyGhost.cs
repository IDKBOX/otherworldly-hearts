using UnityEngine;

public class FriendlyGhost : MonoBehaviour
{
    Animator animator;
    ParticleSystem ps;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        ps = GetComponentInChildren<ParticleSystem>();
    }

    public void Disappear()
    {
        animator.SetTrigger("Disappear");
        ps.Stop();
        Destroy(gameObject, 2f);
    }
}
