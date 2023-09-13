using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Scriptable Character")]
public class ScriptableCharacter : ScriptableObject
{
    public string characterName;

    [Header("Sprites")]
    public Sprite defaultSprite;
    public Sprite smileSprite;
    public Sprite sadSprite;

    public AudioSource[] audioClips;
}
