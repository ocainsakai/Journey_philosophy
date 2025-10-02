using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerHelper : MonoBehaviour
{
    public void Quit() => GameManager.instance?.Quit();
    public void LoadMainMenu() => GameManager.instance?.LoadMainMenu();

    public void LoadSccene(SceneAsset sceneAsset)
    {
        SceneManager.LoadScene(sceneAsset.name);
    }
}
