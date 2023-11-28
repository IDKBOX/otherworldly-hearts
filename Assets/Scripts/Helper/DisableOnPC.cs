using UnityEngine;

public class DisableOnPC : MonoBehaviour
{
#if UNITY_STANDALONE || UNITY_EDITOR
    void Start()
    {
        gameObject.SetActive(false);
    }
#endif
}
