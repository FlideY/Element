using Unity.VisualScripting;
using UnityEngine;
using Zenject;
public class HealthComponent : MonoBehaviour
{
    [SerializeField] PlayerData _playerData;
    [Inject] UIManager uIManager;
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag is "Bullet")
        {
            ChangeHealth(-1);
        }
    }

    void OnCollisionStay2D(Collision2D other)
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
        uIManager.ChangePlayerHealth((float)CurrentHealth / _playerData.MaxHealth);

        Debug.Log($"CurrentHealth: {CurrentHealth}");

        if (CurrentHealth <= 0)
        {
            gameObject.SetActive(false);
            GameManager.Handler();
        }
    }
}