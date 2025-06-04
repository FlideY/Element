using MyFiniteStateMachine;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected FSM fsm = new();
    protected int _health;
}