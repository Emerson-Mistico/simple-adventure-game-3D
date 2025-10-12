using System.Collections.Generic;
using UnityEngine;

namespace ESM.StateMachine
{
    public class StateMachine<T> where T : System.Enum
    {
        public Dictionary<T, StateBase> dictionaryState;

        private StateBase _currentState;
        public float timeToStartGame = 1f;

        public StateBase CurrentState
        {
            get { return _currentState; }
        }
        public void Init()
        {
            dictionaryState = new Dictionary<T, StateBase>();
        }
        public void RegisterStates(T typeenum, StateBase state)
        {
            dictionaryState.Add(typeenum, state);
        }
        public void SwitchState(T state, params object[] objects)
        {
            if (_currentState != null)
            {
                _currentState.OnStateExit();
            }

            _currentState = dictionaryState[state];

            _currentState.OnStateEnter(objects);

        }
        public void Update()
        {
            if (_currentState != null)
            {
                _currentState.OnStateStay();
            }
        }
    }
}
