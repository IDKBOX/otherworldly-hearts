using DG.Tweening;
using UnityEngine;

public class GhostFollow : MonoBehaviour
{
    public Transform target;
    public Transform player;
    public float moveSpeed;
    public GameObject ghostLight;

    private ParticleSystem starParticle;

    private void Awake()
    {
        starParticle = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void DashRefreshIndicator()
    {
        ghostLight.SetActive(true);
        starParticle.Play();
        moveSpeed /= 1.5f;
        transform.DOPunchScale(new Vector3(1.5f * transform.localScale.x, 1.5f, 1.5f), 0.5f);
    }

    public void DashUsedIndicator()
    {
        ghostLight.SetActive(false);
        moveSpeed *= 1.5f;
    }
}
