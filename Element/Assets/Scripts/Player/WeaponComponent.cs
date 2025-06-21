using UnityEngine;
using Zenject;

public class WeaponComponent : MonoBehaviour
{
    [SerializeField] PlayerData _playerData;
    [Inject] EraserManager _eraserManager;

    float _coolDown = 0.25f;
    float _timer;

    void Start()
    {
        _timer = _coolDown;
    }
    void Update()
    {
        if(_timer > 0) _timer -= Time.deltaTime;
    }

    public void Hit()
    { 
        if (_timer < 0)
        {
            _eraserManager.Hit();
            _timer = _coolDown;
        } 
    }
}
