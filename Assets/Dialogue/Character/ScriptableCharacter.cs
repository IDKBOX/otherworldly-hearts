using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Scriptable Character")]
public class ScriptableCharacter : ScriptableObject
{
    public string characterName;

    [Header("Sprites")]
    public Sprite[] portraitImages;

    public AudioSource[] audioClips;
}
