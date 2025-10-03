using UnityEngine;

[CreateAssetMenu(fileName = "KnowledgeData", menuName = "Scriptable Objects/KnowledgeData")]
public class KnowledgeData : ScriptableObject
{
    [TextArea]
    public string title;

    [TextArea(10, 20)]
    public string content;

    public Sprite philosopherImage;
    public AudioClip soundEffect;
}
