using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Enemy
{
    public class EnemyShoot : EnemyBase
    {
        public GunBase gunBase;

        protected override void Init()
        {
            base.Init();
            // gunBase.StartShoot();
        }
    }
}


