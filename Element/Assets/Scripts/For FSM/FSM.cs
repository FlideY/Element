using System;
using System.Collections.Generic;

namespace MyFiniteStateMachine
{
    public class FSM
    {
        public FsmState CurrentState { get; private set; }
        private Dictionary<Type, FsmState> states = new Dictionary<Type, FsmState>();

        public void AddState(FsmState state) => states.TryAdd(state.GetType(), state);
        public void SetState<T>() where T : FsmState
        {
            var type = typeof(T);

            if (CurrentState != null && CurrentState.GetType() == type)
                return;

            if (states.TryGetValue(type, out var newState))
            {
                CurrentState?.Exit();
                CurrentState = newState;
                CurrentState.Enter();
            }
        }
        public void Update() => CurrentState?.Update();
        public void FixedUpdate() => CurrentState?.FixedUpdate();
    }

    public abstract class FsmState
    {
        protected readonly FSM FSM;
        public FsmState(FSM fsm) => FSM = fsm;

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
    }
}