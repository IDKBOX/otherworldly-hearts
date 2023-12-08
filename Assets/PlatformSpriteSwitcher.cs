using UnityEngine;

public class PlatformSpriteSwitcher : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite PCSprite;
    public Sprite mobileSprite;

    private void Start()
    {
#if UNITY_ANDROID || UNITY_IOS
        spriteRenderer.sprite = mobileSprite;

#elif UNITY_STANDALONE
        spriteRenderer.sprite = PCSprite;

#endif
    }
}
