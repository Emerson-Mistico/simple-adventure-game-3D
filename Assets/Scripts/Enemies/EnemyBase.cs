using DG.Tweening;
using UnityEngine;
using Animation;


namespace Enemy 
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {        
        public float startLife = 10f;       

        public float startAnimationDuration = .2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;
        public Collider collier;

        [SerializeField] private AnimationBase _animationBase;
        [SerializeField] private float _currentLife;

        private void Awake()
        {
            Init();
        }

        protected void ResetLife()
        {
            _currentLife = startLife;
        }

        protected virtual void Init()
        {
            ResetLife();
            if (startWithBornAnimation) 
            {
                BornAnimation();
            }
            
        }
        protected virtual void Kill()
        {
            OnKill();
        }

        protected virtual void OnKill() 
        { 
            if (collier != null)
            {
                collier.enabled = false;
            }
            Destroy(gameObject, 1.4f);
            PlayAnimationByTrigger(AnimationType.DEATH);
        }

        public void OnDamage(float f)
        {
            _currentLife -= f;

            if (_currentLife <= 0) 
            {
                Kill();
            }

        }

        #region ANIMATION
        private void BornAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger(animationType);
        }

        #endregion

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                OnDamage(5f);
            }
        }

        public void Damage(float damage)
        {
            Debug.Log("Tomou dano de: " + damage + " pts.");
            OnDamage(damage);
        }
    }
}
