using UnityEngine;

public class WeaponComponent : MonoBehaviour
{
    [SerializeField] PlayerData _playerData;
    EraserManager _eraserManager;

    float _coolDown = 0.25f;
    float _timer;

    void Start()
    {
        _timer = _coolDown;
        _eraserManager = GameObject.Find("EraserManager").GetComponent<EraserManager>();
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
