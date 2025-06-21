using UnityEngine;
using CustomPool;
using Zenject;

public class BossManager : MonoBehaviour
{
    [Inject] Room _room;
    [SerializeField] Transform _parent;
    public delegate void EventHandler();
    public static EventHandler Handler;

    [Inject] Boss _bossPrefab;
    CustomPool<Boss> _bossPool;

    void Start()
    {
        Handler = OnEnterBossRoom;
        _bossPool = new CustomPool<Boss>(_bossPrefab, _parent);
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
