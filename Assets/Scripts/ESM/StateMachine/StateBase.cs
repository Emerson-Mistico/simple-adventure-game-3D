using UnityEngine;

namespace ESM.StateMachine
{
    public class StateBase
    {
        public virtual void OnStateEnter(params object[] objects)
        {
            //Debug.Log("OnStateEnter");
        }

        public virtual void OnStateStay()
        {
            //Debug.Log("OnStateStay");
        }

        public virtual void OnStateExit()
        {
            //Debug.Log("OnStateExit");
        }

    }
}
