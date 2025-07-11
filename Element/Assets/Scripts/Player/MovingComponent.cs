using UnityEngine;

public class MovingComponent : MonoBehaviour
{
    public PlayerData PlayerData;
    [SerializeField] VariableJoystick _joystick;
    Vector2 _moveInput;
    public Vector2 LastInput { get; private set; }
    Rigidbody2D _rb2D;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        LastInput = new Vector2(1, -1);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        _moveInput.x = _joystick.Horizontal;

        _moveInput.y = _joystick.Vertical;
        if (_moveInput != Vector2.zero)
        {
            LastInput = _moveInput;
        }
        if (_rb2D.linearVelocityX < 0) spriteRenderer.flipX = true;
        else if (_rb2D.linearVelocityX > 0) spriteRenderer.flipX = false;
        
    }
    void FixedUpdate() => Move();
    void Move()
    {
        Vector2 movement;
        Vector2 targetSpeed = _moveInput.normalized * PlayerData.Maxspeed; // Нужная скорость

		Vector2 speedDif = targetSpeed - _rb2D.linearVelocity; // Разница в скоростях на данный момент(кадр)
        if(_moveInput == Vector2.zero)
            movement = speedDif * PlayerData.Deceleration;
        else
		    movement = speedDif * PlayerData.Acceleration;            

		_rb2D.AddForce(movement, ForceMode2D.Force);
    }
}
