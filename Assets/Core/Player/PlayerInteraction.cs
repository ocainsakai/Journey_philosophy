using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI textMeshPro;

    public void SetAction(string title, Action action)
    {
        button.gameObject.SetActive(true);
        textMeshPro.text = title;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            action?.Invoke();
            button.gameObject.SetActive(false);
        });
    }
}