using UnityEngine;

public class RoomData : ScriptableObject
{
    public bool IsPassed = false;
    [SerializeField] protected string _tag;
    public int EnvironmentIndex { get; set; }
    public Vector2Int RoomIndex;
    public string Tag 
    {
        get {return _tag;}
        set {_tag = value;}
    }
}