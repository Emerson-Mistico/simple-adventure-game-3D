using UnityEngine;
using ESM.StateMachine;

public class FSMExample : MonoBehaviour
{
    
    public enum ExempleEnum
    {
        STATE_ONE,
        STATE_TWO,
        STATE_THREE
    }

    public StateMachine<ExempleEnum> stateMachine;

    private void Start()
    {
        //stateMachine = new StateMachine<ExempleEnum>(ExempleEnum.STATE_ONE);
        stateMachine = new StateMachine<ExempleEnum>();
        stateMachine.Init();
        stateMachine.RegisterStates(ExempleEnum.STATE_ONE, new StateBase());
        stateMachine.RegisterStates(ExempleEnum.STATE_TWO, new StateBase());
        stateMachine.RegisterStates(ExempleEnum.STATE_THREE, new StateBase());
    }

}
