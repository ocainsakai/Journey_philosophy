using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
[CreateAssetMenu(fileName = "MapMetaData", menuName = "Scriptable Objects/MapMetaData")]

public class MapMetaData : ScriptableObject
{
    public string Name;
    public Sprite Art;
    public SceneAsset scene;
}
