using UnityEngine;
using UnityEngine.AI;
public class CircleEnemy : Enemy
{
    Transform player;
    NavMeshAgent agent;
    Animator animator;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= 25)
        {
            agent.SetDestination(player.position);
            animator.SetFloat("Blend", 1.00f);
            return;
        }
        else
        {
            animator.SetFloat("Blend", 0.00f);
            return;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 25);
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
        _health = 1;
    }
    void OnDisable()
    {
        EnemyManager.Handler();
    }
}