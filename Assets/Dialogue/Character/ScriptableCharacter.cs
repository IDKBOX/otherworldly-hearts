using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Scriptable Character")]
public class ScriptableCharacter : ScriptableObject
{
    public string characterName;
    public Sprite[] sprites;
    public AudioSource[] audioClips;
}
