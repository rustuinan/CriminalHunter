using UnityEngine;

[CreateAssetMenu(fileName = "New Character Story", menuName = "Character/Story")]
public class CharacterStory : ScriptableObject
{
    public string characterName;  // Karakterin adı
    [TextArea]
    public string story;          // Karakterin hikayesi
    public bool isCriminal;       // Suçlu mu, suçsuz mu
    public GameObject characterPrefab;  // Karakterin oyun dünyasındaki görsel temsili (Prefab)
}
