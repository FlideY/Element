using UnityEngine;
public class CircleEnemy : Enemy
{
    Transform player;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        fsm.AddState(new CircleEnemyIdle(fsm));
        fsm.AddState(new CircleEnemyFollow(fsm, player, transform));
        fsm.SetState<CircleEnemyIdle>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < 35) fsm.SetState<CircleEnemyFollow>();
        else fsm.SetState<CircleEnemyIdle>();
        fsm.Update();
    }

    void FixedUpdate()
    {
        fsm.FixedUpdate();
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 20);
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
        if (_health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        _health = 5;
    }
    void OnDisable()
    {
        EnemyManager.Handler();
    }
}