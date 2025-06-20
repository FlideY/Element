using UnityEngine;
using CustomPool;

public class Boss : Enemy
{
    Transform _parent;
    Transform _player;
    [SerializeField] Projectile _projectilePrefab;
    CustomPool<Projectile> _projectiles;
    float _timer;
    float _coolDown = 4;
    void Start()
    {
        _player = GameObject.Find("Player").transform;
        _parent = GameObject.Find("Projectiles").transform;
        _projectiles = new CustomPool<Projectile>(_projectilePrefab, _parent);
        _timer = _coolDown;
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