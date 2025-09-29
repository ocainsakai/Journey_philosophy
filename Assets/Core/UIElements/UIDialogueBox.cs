using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDialogueBox : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _dialogueContent;
    public float textSpeed = 0.1f;
    public List<string> lines;
    int index;
    public Action OnLineComplete;

    private Coroutine typingCoroutine;

    public void AddLine(string line)
    {
        line = line.Trim();
        lines.Add(line);
    }

    public void NewLine(string line)
    {
        lines.Clear();
        AddLine(line);
    }

    public IEnumerator StartDialogue()
    {
        StopAllCoroutines();
        index = 0;
        _dialogueContent.text = string.Empty;
        typingCoroutine = StartCoroutine(TypeLine());
        yield return typingCoroutine;
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            _dialogueContent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        typingCoroutine = null;
        OnLineComplete?.Invoke();

    }

    public void NextLine()
    {
        index++;
        if (index < lines.Count)
        {
            _dialogueContent.text = string.Empty;
            typingCoroutine = StartCoroutine(TypeLine());
        }
    }

    public void EndDialogue()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }
        _dialogueContent.text = lines[index]; 
        OnLineComplete?.Invoke();
    }
}
