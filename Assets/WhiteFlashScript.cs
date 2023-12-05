using UnityEngine;

public class WhiteFlashScript : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Destroy(gameObject, 5f);
    }
}
