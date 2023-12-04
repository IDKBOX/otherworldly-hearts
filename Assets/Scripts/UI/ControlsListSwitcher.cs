using UnityEngine;
using UnityEngine.UI;

public class ControlsListSwitcher : MonoBehaviour
{
    public Image controlImage;
    public Sprite PCControls;
    public Sprite mobileControls;

    private void Start()
    {
#if UNITY_ANDROID || UNITY_IOS
        controlImage.sprite = mobileControls;

#elif UNITY_STANDALONE
        controlImage.sprite = PCControls;

#endif
    }
}
