using UnityEngine;
using CustomPool;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("MiniMap")]
    [SerializeField] Transform _miniMap;
    [SerializeField] RoomUI _roomUI;

    [Header("BossPanel")]
    public GameObject BossPanel;
    [SerializeField] RectTransform _bossHealthBar;

    [Header("NextLevelButton")]
    public GameObject NextLevelButton;
    public TextMeshProUGUI _nextLevelButttonText;

    CustomPool<RoomUI> _roomsOnMiniMap;
    void Awake()
    {
        _roomsOnMiniMap = new(_roomUI, _miniMap);
    }

    public void DrawMiniMap()
    {
        foreach (var room in LevelGenerator.FormalRooms)
        {
            int x = room.Key.x;
            int y = room.Key.y;

            RoomUI roomUI = _roomsOnMiniMap.Get();
            roomUI.GetComponent<RectTransform>().SetLocalPositionAndRotation
            (new Vector3(x * 50 + 28, y * 50 + 28, 0), Quaternion.identity);
        }
    }

    public void ChangeBossHealth(float _healthPercant)
    {
        _bossHealthBar.sizeDelta = new Vector2(1000 * _healthPercant, _bossHealthBar.sizeDelta.y);
    }
}