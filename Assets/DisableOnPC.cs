using UnityEngine;

public class DisableOnPC : MonoBehaviour
{
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
    void Start()
    {
        gameObject.SetActive(false);
    }
#endif
}
