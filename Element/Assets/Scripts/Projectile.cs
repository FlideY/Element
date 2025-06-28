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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag is "Player" ||  other.gameObject.tag is "Environment" ||
        other.gameObject.tag is "TopDoor" || other.gameObject.tag is "LeftDoor" ||
        other.gameObject.tag is "RightDoor" || other.gameObject.tag is "BottomDoor")
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