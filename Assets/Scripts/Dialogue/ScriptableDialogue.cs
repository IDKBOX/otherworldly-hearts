using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Scriptable Dialogue")]
public class ScriptableDialogue : ScriptableObject
{
    [Header("Type")]
    public bool isDescriptionDialogue;

    [Header("Lines")]
    public string[] lines;

    [Header("Portrait Dialogue Options")]
    public ScriptableCharacter characterData;
    public int[] emotion;
}
