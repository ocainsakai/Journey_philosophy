using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerHelper : MonoBehaviour
{
    public void Quit() => GameManager.Instance?.Quit();
    public void LoadMainMenu() => GameManager.Instance?.LoadMainMenu();

    public void LoadSccene(SceneAsset sceneAsset)
    {
        SceneManager.LoadScene(sceneAsset.name);
        SavePlayer();
    }

    private void SavePlayer()
    {
        PlayerController.Instance.Inventory.SaveData();
    }
}
