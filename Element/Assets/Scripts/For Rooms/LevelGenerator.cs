using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("LevelParametrs")]
    [SerializeField] Vector2Int _gridSize;
    [SerializeField] int _minRooms = 8;
    [SerializeField] int _maxRooms = 10;

    int _roomCount = 0;
    int _bossroomCount = 0;
    int _amountOfRooms;
    Queue<Vector2Int> _roomQueue = new();

    public static bool GenerationComplete = false;
    public static int[,] RoomGrid = new int[5, 5];
    public static Dictionary<Vector2Int, string> FormalRooms = new();
    public static Vector2Int StartRoomIndex = new(2, 2);

    void Start()
    {
        StartRoomGenerationFromRoom(StartRoomIndex);
        do { Generate(); }
        while (!GenerationComplete);
    }

    void Generate()
    {
        if (_roomQueue.Count > 0 && _roomCount < _amountOfRooms && !GenerationComplete)
        {
            Vector2Int roomIndex = _roomQueue.Dequeue();
            int x = roomIndex.x;
            int y = roomIndex.y;
            try { TryGenerateRoom(new Vector2Int(x - 1, y)); }
            catch (System.IndexOutOfRangeException){ }
            try { TryGenerateRoom(new Vector2Int(x + 1, y)); }
            catch (System.IndexOutOfRangeException){ }
            try { TryGenerateRoom(new Vector2Int(x, y - 1)); }
            catch (System.IndexOutOfRangeException){ }
            try { TryGenerateRoom(new Vector2Int(x, y + 1)); }
            catch (System.IndexOutOfRangeException){ }
        }
        else if (_roomCount < _minRooms || _bossroomCount != 1 && !GenerationComplete)
        {
            RegenerateRooms();
        }
        else if (!GenerationComplete)
        {
            GameObject.Find("RoomManager").GetComponent<RoomManager>().CreateDatas();
            GameObject.Find("UIManager").GetComponent<UIManager>().DrawMiniMap();
            GenerationComplete = true;
        }
    }

    void StartRoomGenerationFromRoom(Vector2Int roomIndex)
    {
        _amountOfRooms = Random.Range(_minRooms, _maxRooms);// Случайное количество комнат на уровне
        _roomQueue.Enqueue(roomIndex); // Добавление в очередь
        int x = roomIndex.x;
        int y = roomIndex.y;
        RoomGrid[x, y] = 1; 
        _roomCount++;

        FormalRooms.Add(roomIndex, "Start");
    }

    void RegenerateRooms()
    {
        RoomGrid = new int[_gridSize.x, _gridSize.y];
        _roomQueue.Clear();
        FormalRooms.Clear();
        _roomCount = 0;
        _bossroomCount = 0;
        GenerationComplete = false;

        StartRoomGenerationFromRoom(StartRoomIndex);
    }

    private bool TryGenerateRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;

        if(_roomCount >= _amountOfRooms)
            return false;
        if(Random.value < 0.5f)
            return false;
        if(CountAdjacentRooms(roomIndex) > 1)
            return false;
        if(RoomGrid[x, y] == 1)
            return false;


        _roomQueue.Enqueue(roomIndex); // Вставляем в очередь
        RoomGrid[x, y] = 1;
        _roomCount++;

        string roomTag;
        if(_roomCount == _amountOfRooms)
        {
            roomTag = "Boss";
            _bossroomCount++;
        }
        else roomTag = "Normal";

        FormalRooms.Add(roomIndex, roomTag);

        return true;
    }
    int CountAdjacentRooms(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;
        int count = 0;

        try { if (RoomGrid[x - 1, y] == 1) count++; }
        catch (System.IndexOutOfRangeException){ }
        try { if (RoomGrid[x + 1, y] == 1) count++; }
        catch (System.IndexOutOfRangeException){ }
        try { if (RoomGrid[x, y - 1] == 1) count++; }
        catch (System.IndexOutOfRangeException){ }
        try { if (RoomGrid[x, y + 1] == 1) count++; }
        catch (System.IndexOutOfRangeException){ }

        return count;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        for(int x = 0; x < _gridSize.x; x++)
        {
            for(int y = 0; y < _gridSize.y; y++)
            {
                Vector2 centerposition = new Vector2Int(x * 100, y * 100);
                Gizmos.DrawWireCube(new Vector3(centerposition.x, centerposition.y, 1), // center
                 new Vector3(100, 100, 1)); // size

                if(RoomGrid[x, y] == 0) 
                    Gizmos.DrawWireSphere(new Vector3(centerposition.x, centerposition.y, 1), 25);
                if(RoomGrid[x, y] == 1) 
                    Gizmos.DrawWireCube(new Vector3(centerposition.x, centerposition.y, 1), new Vector3(40, 40, 1));
            }
        }
    }
}