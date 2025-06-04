using UnityEngine;

public class Eraser : Weapon
{
    [HideInInspector] public Rigidbody2D Rb2D;
    Transform _player;
    int _angle = 0;
    float _timer;
    float _deleteTime = 6;

    void Start()
    {
        _player = GameObject.Find("Player").transform;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag is "Enemy" ||  other.tag is "Environment" ||
        other.tag is "TopDoor" || other.tag is "LeftDoor" || other.tag is "RightDoor" || other.tag is "BottomDoor")
        {
            gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        Rb2D = GetComponent<Rigidbody2D>();
        _timer = _deleteTime;
    }
    void OnDisable()
    {
        transform.position = _player.position;
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            gameObject.SetActive(false);
        }
    }
    void FixedUpdate()
    {
        _angle += 8;
        Rb2D.MoveRotation(_angle);
    }
}