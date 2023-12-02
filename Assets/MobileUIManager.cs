using UnityEngine;

public class MobileUIManager : MonoBehaviour
{
    public static MobileUIManager Instance;
    public GameObject mobileUI;
    public bool debugOnPC;
    private bool isEnabled = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

#if UNITY_STANDALONE || UNITY_EDITOR
    private void Start()
    {
        if (!debugOnPC)
        {
            isEnabled = false;
            mobileUI.SetActive(false);
        }
    }
#endif

    public void hideMobileUI()
    {
        if (isEnabled)
        {
            mobileUI.SetActive(false);
        }
    }

    public void showMobileUI()
    {
        if (isEnabled)
        {
            mobileUI.SetActive(true);
        }
    }
}
