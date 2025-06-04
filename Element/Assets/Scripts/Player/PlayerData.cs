using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData")]
public class PlayerData : ScriptableObject
{ 
    [Header("Movement")]
    [SerializeField] float _maxSpeed;
	[SerializeField] float _acceleration;
	[SerializeField] float _deceleration;

	[Header("Health")]
	[SerializeField] int _maxHealth;

	public float Maxspeed { get { return _maxSpeed; } }
	public float Acceleration { get{return _acceleration; }}
	public float Deceleration {get{return _deceleration; }}

	public int MaxHealth { get { return _maxHealth; } }

    void OnValidate()
    {
		_maxSpeed = Mathf.Clamp(_maxSpeed, 0.01f, 40);
		_acceleration = Mathf.Clamp(Acceleration, 0.01f, _maxSpeed);
		_deceleration = Mathf.Clamp(Deceleration, 0.01f, _maxSpeed);
	}
}