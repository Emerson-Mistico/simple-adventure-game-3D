using UnityEngine;
using ESM.StateMachine;

namespace Boss
{
    public class BossStateBase : StateBase
    {
        protected BossBase boss;

        public override void OnStateEnter(params object[] objects)
        {
            base.OnStateEnter(objects);
            boss = (BossBase)objects[0];
        }

    }   

    public class BossStateInit : BossStateBase
    {
        public override void OnStateEnter(params object[] objects)
        {
            base.OnStateEnter(objects);
            boss.StartInitAnimation();
            boss.SwtichState(BossAction.WALK);
        }
    }
    public class BossStateWalk : BossStateBase
    {
        public override void OnStateEnter(params object[] objects)
        {
            base.OnStateEnter(objects);
            boss.GoToRandomPoint(OnArrive);            
        }
        private void OnArrive()
        {
            boss.SwtichState(BossAction.ATTACK);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            boss.StopAllCoroutines();
        }

    }
    public class BossStateAttack : BossStateBase
    {
        public override void OnStateEnter(params object[] objects)
        {
            base.OnStateEnter(objects);
            boss.StartAttack(EndAttacks);          
        }

        private void EndAttacks() 
        {
            boss.SwtichState(BossAction.WALK);
        }
        public override void OnStateExit()
        {
            base.OnStateExit();
            boss.StopAllCoroutines();
        }

    }
    public class BossStateDeath : BossStateBase
    {
        public override void OnStateEnter(params object[] objects)
        {
            base.OnStateEnter(objects);
            // boss.transform.localScale = Vector3.one * .2f;
        }        

    }
    
}


