using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Scriptable Objects/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [Header("Text in Rooms")]
    public string StartText;
    public string BossText;
    [Header("UI Text")]
    public string NextButtonLevelText;
    
}
