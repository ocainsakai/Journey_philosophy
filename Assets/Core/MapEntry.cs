using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapEntry : MonoBehaviour
{
    public Button button;
    public Image artComponent;
    public TextMeshProUGUI _textComponent;
    public string _sceneName;

    public void SetData(Sprite art, string mapName, string sceneName, bool isInteractable)
    {
        artComponent.sprite = art;
        _sceneName = sceneName;
        _textComponent.text = mapName;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(sceneName);
        });
        button.interactable = isInteractable;
    }

}