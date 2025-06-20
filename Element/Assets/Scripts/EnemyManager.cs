using UnityEngine;
using CustomPool;

public class EnemyManager : MonoBehaviour
{
    public delegate void EventHandler();
    public static EventHandler Handler;

    [SerializeField] Room _room;
    [SerializeField] Transform _parent;
    [SerializeField] Enemy _enemyPrefab;
    [SerializeField] Transform[] _spawnPositions;

    CustomPool<Enemy> _enemyPool;

    void Start()
    {
        Handler = OnEnterNormalRoom;
        _enemyPool = new CustomPool<Enemy>(_enemyPrefab, _parent);
    }
    void OnEnterNormalRoom()
    {
        if (!_room.RoomData.IsPassed)
        {
            Handler = OnEnemyKill;
            _room.CloseDoors();
            SpawnWave();
        }
    }

    void OnEnemyKill()
    {
        if (AllEnemiesKilled())
        {
            _room.ReUnlockDoors();
            _room.RoomData.IsPassed = true;
            Handler = OnEnterNormalRoom;
        }
    }

    bool AllEnemiesKilled()
    {
        foreach (Enemy enemy in _enemyPool.Objects)
        {
            if (enemy.gameObject.activeSelf) return false;
        }
        return true;
    }

    void SpawnWave()
    {
        foreach (Transform position in _spawnPositions)
        {
            Enemy enemy = _enemyPool.Get();
            enemy.transform.position = position.position;
        }
    }
}