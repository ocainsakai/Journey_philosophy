using UnityEngine;

public class GameManagerHelper : MonoBehaviour
{
    public void Quit() => GameManager.instance?.Quit();
    public void LoadMainMenu() => GameManager.instance?.LoadMainMenu();
}
