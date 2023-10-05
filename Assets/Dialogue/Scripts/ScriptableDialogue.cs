using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Scriptable Dialogue")]
public class ScriptableDialogue : ScriptableObject
{
    public string[] lines;
    [Space]
    public bool isDescriptionDialogue;
    public bool isPortraitDialogue;
    public ScriptableCharacter characterData;
    public string[] emotion;
    public GameObject choiceOption;
}
