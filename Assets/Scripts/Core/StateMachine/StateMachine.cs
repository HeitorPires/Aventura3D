using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


namespace Core.StateMachine
{
    public class StateMachine<T> where T : System.Enum
    {

        public Dictionary<T, StateBase> dictionaryState;

        private StateBase _currentState;
        public float timeToStartGame = 1f;

        public StateBase CurrentState { get { return _currentState; } }

        public StateMachine()
        {
            dictionaryState = new Dictionary<T, StateBase>();
        }

        public void Init()
        {
        }

        public void RegisterStates(T typeEnum, StateBase state)
        {
            dictionaryState.Add(typeEnum, state);
        }



        public void SwitchStates(T state)
        {
            if (_currentState != null) _currentState.OnStateExit();

            _currentState = dictionaryState[state];
            _currentState.OnStateEnter();
        }

        // Update is called once per frame
        void Update()
        {
            if (_currentState != null) _currentState.OnStateStay();

        }
    }
}
