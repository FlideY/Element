using UnityEngine;

public class Projectile : MonoBehaviour
{
    Transform _boss;
    public Rigidbody2D Rb2D;
    float _timer;
    float _deleteTime = 20;
    
    
    void OnEnable()
    {
        _boss = GameObject.FindFirstObjectByType<Boss>().transform;
        _timer = _deleteTime;
        transform.position = _boss.position; 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag is "Player" ||  other.tag is "Environment" ||
        other.tag is "TopDoor" || other.tag is "LeftDoor" || other.tag is "RightDoor" || other.tag is "BottomDoor")
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
    }
}