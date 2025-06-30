using UnityEngine;
using CustomPool;
using Zenject;

public class BossManager : MonoBehaviour
{
    [Inject] public UIManager UIManager;
    [Inject] public AudioManager audioManager;
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
        if (!_room.RoomData.IsPassed)
        {
            audioManager.PlayClip(audioManager.OnEnterBossRoom);
            Handler = OnBossKill;
            _room.CloseDoors();
            SpawnBoss();
            UIManager.BossPanel.SetActive(true);

        }
    }

    void OnBossKill()
    {
        Handler = OnEnterBossRoom;
        UIManager.NextLevelButton.SetActive(true);
        _room.ReUnlockDoors();
        _room.RoomData.IsPassed = true;
    }
    
    void SpawnBoss()
    {
        _bossPool.Get();
    }
}
