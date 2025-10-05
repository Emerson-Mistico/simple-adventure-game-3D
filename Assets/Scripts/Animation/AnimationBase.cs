using Animation;
using System.Collections.Generic;
using UnityEngine;

namespace Animation 
{
    
    public enum AnimationType
    {
        NONE,
        IDLE,
        RUN,
        ATTACK,
        DEATH
    }
    
    public class AnimationBase : MonoBehaviour
    {
        public Animator animator;
        public List<AnimationSetup> animationSetups;

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            var setup = animationSetups.Find(i => i.anymationType == animationType);
            if (setup != null)
            {
                animator.SetTrigger(setup.trigger);
            }
        }

    }
}

[System.Serializable]
public class AnimationSetup
{
    public AnimationType anymationType;
    public string trigger;
}

