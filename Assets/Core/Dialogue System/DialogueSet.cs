using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSet", menuName = "Scriptable Objects/DialogueSet")]
public class DialogueSet : ScriptableObject
{
    public List<DialogueNode> dialoguesEntries;
}
