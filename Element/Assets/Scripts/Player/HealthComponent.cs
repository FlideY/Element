using UnityEngine;
public class HealthComponent : MonoBehaviour
{
    [SerializeField] PlayerData _playerData;
    public int CurrentHealth { get; private set; }


    bool _isInvincible;
    float _damageCooldown;


    void Start()
    {
        _isInvincible = false;
        CurrentHealth = _playerData.MaxHealth;
    }
    void Update()
    {
        if (_isInvincible)
        {
            _damageCooldown -= Time.deltaTime;
            if (_damageCooldown < 0) _isInvincible = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag is "Enemy")
        {
            ChangeHealth(-1);
        }
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (_isInvincible) return;
            _isInvincible = true;
            _damageCooldown = 2;
        }
        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, _playerData.MaxHealth);

        if (CurrentHealth <= 0) GameManager.Handler();
    }
}