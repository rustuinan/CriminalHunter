using UnityEngine;

[CreateAssetMenu(fileName = "New Constitution Article", menuName = "Constitution/Article")]
public class ConstitutionArticle : ScriptableObject
{
    [TextArea]
    public string articleText;
    public int unlockLevel;
}
