using UnityEngine;
using CustomPool;
using System.Collections;

public class Boss : Enemy
{
    Rigidbody2D _rb2D;
    BossManager _bossManager;
    Transform _parent;
    Transform _player;
    [SerializeField] Projectile _projectilePrefab;
    CustomPool<Projectile> _projectiles;

    int _maxHealth = 10;
    bool _isCoroutineEnd = true;
    IEnumerator _enumerator;

    void Start()
    {
        _player = GameObject.Find("Player").transform;
        _parent = GameObject.Find("Projectiles").transform;
        _projectiles = new CustomPool<Projectile>(_projectilePrefab, _parent);
        _rb2D = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        _health = _maxHealth;
        _bossManager = GameObject.Find("BossManager").GetComponent<BossManager>();
    }

    void Update()
    {
        if (_isCoroutineEnd)
        {
            _isCoroutineEnd = false;
            int randomIndex = Random.Range(0, 2);
            if (randomIndex == 0) StartCoroutine(CircleAttacking());
            else StartCoroutine(DashingToPlayer());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag is "Eraser")
        {
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

    IEnumerator CircleAttacking()
    {
        Debug.Log("Start of coroutine");
        _rb2D.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(2);
        CircleAttack();
        yield return new WaitForSeconds(2);
        _rb2D.linearVelocityX = 10;
        _rb2D.angularVelocity = 100;
        yield return new WaitForSeconds(1);
        _rb2D.linearVelocity = Vector2.zero;
        _rb2D.angularVelocity = 0;
        yield return new WaitForSeconds(2);
        CircleAttack();
        yield return new WaitForSeconds(2);
        _rb2D.linearVelocityX = -10;
        _rb2D.angularVelocity = -100;
        yield return new WaitForSeconds(1);
        _rb2D.linearVelocity = Vector2.zero;
        _rb2D.angularVelocity = 0;

        _isCoroutineEnd = true;

        Debug.Log("End of coroutine");
    }

    IEnumerator DashingToPlayer()
    {
        Debug.Log("Start of coroutine");

        yield return new WaitForSeconds(3);
        Vector2 direction;
        direction = _player.position - transform.position;
        direction = direction.normalized;
        _rb2D.AddForce(direction * 50, ForceMode2D.Impulse);
        yield return new WaitForSeconds(3);
        direction = _player.position - transform.position;
        direction = direction.normalized;
        _rb2D.AddForce(direction * 50, ForceMode2D.Impulse);
        yield return new WaitForSeconds(3);
        direction = _player.position - transform.position;
        direction = direction.normalized;
        _rb2D.AddForce(direction * 50, ForceMode2D.Impulse);

        _isCoroutineEnd = true;

        Debug.Log("End of coroutine");
    }

    IEnumerator ChasingAndShooting()
    { 
        Debug.Log("Start of coroutine");

        yield return new WaitForSeconds(3);
        

        _isCoroutineEnd = true;

        Debug.Log("End of coroutine");
    }

    void CircleAttack()
    {
        Projectile[] projectiles = new Projectile[12];
        Vector2 direction = Vector2.down;
        for (int i = 0; i < projectiles.Length; i++)
        {
            projectiles[i] = _projectiles.Get();
            direction += new Vector2(-direction.y, direction.x) / Mathf.Sqrt(3);
            direction = direction.normalized;
            projectiles[i].Rb2D.AddForce(direction * 15, ForceMode2D.Impulse);
        }
    }
}