using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    Room _room;
    [SerializeField] Dictionary<Vector2Int, RoomData> _roomDatas = new();

    void Awake()
    {
        _room = GameObject.Find("Room").GetComponent<Room>();
    }

    public void CreateDatas()
    {
        foreach (var room in LevelGenerator.FormalRooms)
        {
            CreateData(room);
        }

        _room.RoomData = _roomDatas[new Vector2Int(2, 2)];
        _room.Build();

    }

    public void SwitchRoom(Vector2Int direction)
    {
        if (LevelGenerator.RoomGrid[_room.RoomData.RoomIndex.x + direction.x, _room.RoomData.RoomIndex.y + direction.y] == 1)
        {
            _room.RoomData = _roomDatas[_room.RoomData.RoomIndex + direction];
            _room.Build();
        }
    }

    void CreateData(KeyValuePair<Vector2Int, string> room)
    {
        switch (room.Value)
        {
            case "Start":
                RoomData roomData = ScriptableObject.CreateInstance<RoomData>();
                roomData.RoomIndex = room.Key;
                roomData.Tag = room.Value;
                roomData.EnvironmentIndex = -1;
                _roomDatas.Add(room.Key, roomData);
                return;
            case "Normal":
                RoomData nRoomData = ScriptableObject.CreateInstance<RoomData>();
                nRoomData.RoomIndex = room.Key;
                nRoomData.Tag = room.Value;
                nRoomData.EnvironmentIndex = Random.Range(0, 3);
                _roomDatas.Add(room.Key, nRoomData);
                return;
            case "Boss":
                RoomData bRoomData = ScriptableObject.CreateInstance<RoomData>();
                bRoomData.RoomIndex = room.Key;
                bRoomData.Tag = room.Value;
                _roomDatas.Add(room.Key, bRoomData);
                return;
        }
    }
}