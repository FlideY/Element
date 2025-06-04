using UnityEngine;
using MyFiniteStateMachine;

public class CircleEnemyFollow: FsmState
{
    Transform _player;
    Transform _enemy;
    public CircleEnemyFollow(FSM fsm, Transform player, Transform enemy) : base(fsm) 
    {
        _player = player;
        _enemy = enemy;
    }
    public override void Enter(){}
    public override void Exit(){}
    public override void Update() => Move();
    public override void FixedUpdate(){}
    public void Move() => _enemy.position = Vector2.MoveTowards(_enemy.position, _player.position, 5 * Time.deltaTime); 
}
