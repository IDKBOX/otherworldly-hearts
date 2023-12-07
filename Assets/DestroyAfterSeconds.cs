using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float time;

    void Start()
    {
        Destroy(gameObject, time);
    }
}
