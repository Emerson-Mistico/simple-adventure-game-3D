using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test
{
    public enum Test2
    {
        NONE
    }

    public void Aa()
    {
        StateMachine<Test2> stateMachine = new StateMachine<Test2>();
        stateMachine.RegisterStates(Test.Test2.NONE, new StateBase());
    }
}
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
    public void SwitchState(T state)
    {
        if (_currentState != null)
        {
            _currentState.OnStateExit();
        }

        _currentState = dictionaryState[state];
        
        _currentState.OnStateEnter(); 
        
    }
    public void Update()
    {
        if (_currentState != null)
        {
            _currentState.OnStateStay();
        }
    }
}