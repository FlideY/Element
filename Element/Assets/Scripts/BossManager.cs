using UnityEngine;
using CustomPool;

public class BossManager : MonoBehaviour
{
    [SerializeField] Room _room;
    public delegate void EventHandler();
    public static EventHandler Handler;
    [SerializeField] Boss _bossPrefab;
    CustomPool<Boss> _bossPool;

    void Start()
    {
        Handler = OnEnterBossRoom;
        _bossPool = new CustomPool<Boss>(_bossPrefab, transform);
    }
    void OnEnterBossRoom()
    {
        Debug.Log("Enter BossRoom");
        if (!_room.RoomData.IsPassed)
        {
            Handler = OnBossKill;
            _room.CloseDoors();
            SpawnBoss();
        }
    }

    void OnBossKill()
    { 

    }
    void SpawnBoss()
    {
        _bossPool.Get();
    }
}
