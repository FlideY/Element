using UnityEngine;
using CustomPool;
using Unity.VisualScripting;

public class EraserManager : MonoBehaviour
{
    public CustomPool<Eraser> _erasers;
    [SerializeField] Eraser _prefabEraser;
    [SerializeField] Transform _player;
    [SerializeField] LayerMask _enemyLayer;
    MovingComponent _movingComponent;

    void Start()
    {
        _movingComponent = GameObject.Find("Player").GetComponent<MovingComponent>();
        _erasers = new CustomPool<Eraser>(_prefabEraser, _player);
    }

    public void Hit()
    {
        Transform enemy = FindEnemy();
        Vector2 miss;
        float koef = Random.Range(-0.2f, 0.2f);

        Eraser eraser = _erasers.Get();
        if (enemy != null)
        {
            Vector2 direction = enemy.position - eraser.transform.position;

            miss = new Vector2(-direction.y, direction.x);
            eraser.Rb2D.AddForce((direction.normalized + miss.normalized * koef) * 2000);
        }
        else
        {
            miss = new Vector2(-_movingComponent.LastInput.y, _movingComponent.LastInput.x);
            eraser.Rb2D.AddForce((_movingComponent.LastInput.normalized + miss.normalized * koef) * 2000);
        }
    }

    Transform FindEnemy()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(_player.position, 40, _enemyLayer.value);
        float distance = Mathf.Infinity;
        Transform nearestEnemy = null;
        foreach (Collider2D enemy in enemies)
        {
            if (Vector3.Distance(_player.position, enemy.transform.position) < distance)
            {
                distance = Vector3.Distance(_player.position, enemy.transform.position);
                nearestEnemy = enemy.transform;
            }
        }
        return nearestEnemy;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_player.transform.position, 40);
    }
}