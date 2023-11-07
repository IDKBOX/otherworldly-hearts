using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class GhostFollow : MonoBehaviour
{
    public Transform target;
    public Transform player;
    public float moveSpeed;

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
}
