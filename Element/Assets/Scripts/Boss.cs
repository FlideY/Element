using UnityEngine;
using CustomPool;

public class Boss : Enemy
{
    BossManager _bossManager;
    Transform _parent;
    //Transform _player;
    [SerializeField] Projectile _projectilePrefab;
    CustomPool<Projectile> _projectiles;
    float _timer;
    float _coolDown = 4;
    int _maxHealth = 10;
    void Start()
    {
        //_player = GameObject.Find("Player").transform;
        _parent = GameObject.Find("Projectiles").transform;
        _projectiles = new CustomPool<Projectile>(_projectilePrefab, _parent);
        _timer = _coolDown;
    }

    void OnEnable()
    {
        _health = _maxHealth;
        _bossManager = GameObject.Find("BossManager").GetComponent<BossManager>();
    }

    void Update()
    {
        if (_timer < 0)
        {
            Attack();
            _timer = _coolDown;
        }
        else _timer -= Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag is "Eraser")
        {
            other.gameObject.SetActive(false);
            TakeDamage(-1);
        }
    }

    void TakeDamage(int damage)
    {
        _health += damage;
        _bossManager.UIManager.ChangeBossHealth((float)_health / _maxHealth);
        if (_health <= 0)
        {
            gameObject.SetActive(false);
            BossManager.Handler();
        }
    }

    void Attack()
    {
        Projectile[] projectiles = new Projectile[12];
        Vector2 direction = Vector2.down;
        for (int i = 0; i < projectiles.Length; i++)
        {
            projectiles[i] = _projectiles.Get();
            direction += new Vector2(-direction.y, direction.x) / Mathf.Sqrt(3);
            direction = direction.normalized;
            projectiles[i].Rb2D.AddForce(direction * 10, ForceMode2D.Impulse);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, 20);
    }
}