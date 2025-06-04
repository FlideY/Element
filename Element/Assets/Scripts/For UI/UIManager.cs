using UnityEngine;
using CustomPool;

public class UIManager : MonoBehaviour
{
    [SerializeField] Transform _miniMap;
    [SerializeField] RoomUI _roomUI;
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
            (new Vector3(x * 50 + 30, y * 50 + 30, 0), Quaternion.identity);
        }
    }
}