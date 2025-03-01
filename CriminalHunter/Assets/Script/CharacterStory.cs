using UnityEngine;

[CreateAssetMenu(fileName = "New Character Story", menuName = "Character/Story")]
public class CharacterStory : ScriptableObject
{
    public string characterName;
    [TextArea]
    public string story;
    public bool isCriminal;
    public GameObject characterPrefab;
}
