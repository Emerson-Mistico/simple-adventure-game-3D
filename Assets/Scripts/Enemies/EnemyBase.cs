using DG.Tweening;
using UnityEngine;
using Animation;
using Unity.VisualScripting;


namespace Enemy 
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        [Header("Enemy Settings")]        
        public float startLife = 10f;
        public GameObject lifeIndicator = null;
        public Collider enemyCollider;

        [Header("Animation Settings")]
        public float startAnimationDuration = .2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;
        public FlashColor flashColor;
        public ParticleSystem particlesToKill;
        public bool lookAtTarget = false;

        [SerializeField] private AnimationBase _animationBase;
        [SerializeField] private float _currentLife;

        private PlayerManager _player;
        private int _playerIsAlive;

        private Vector3 _lifeInitScale;
        private Vector3 _lifeInitLocalPos;
        private Vector3 _initialEnemyPosition;

        private void Awake()
        {
            Init();
            if (lifeIndicator != null) 
            { 
                lifeIndicator.SetActive(true);

                _lifeInitScale = lifeIndicator.transform.localScale;
                _lifeInitLocalPos = lifeIndicator.transform.localPosition;
                _initialEnemyPosition = this.transform.localPosition;

            }
        }
        public virtual void Update()
        {
            _playerIsAlive = PlayerPrefs.GetInt("PlayerIsAlive");

            if (lookAtTarget && _playerIsAlive == 1)
            {
                transform.LookAt(_player.transform.position);
            }

            if (IsAlive() && PlayerPrefs.GetInt("PlayerIsAlive") == 0)
            {
                _currentLife = startLife;
                this.transform.localPosition = _initialEnemyPosition;
                UpdateLifeIndicatorAnimated();
            }
        }

        private void Start()
        {
            // _player = GameObject.FindObjectOfType<PlayerManager>();
            _player = Object.FindAnyObjectByType<PlayerManager>();
        }

        protected void ResetLife()
        {
            _currentLife = startLife;
        }
        public bool IsAlive()
        {
            return _currentLife > 0;
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
            if (enemyCollider != null)
            {
                enemyCollider.enabled = false;
            }
            if (particlesToKill != null)
            {
                particlesToKill.Emit(15);
            }
            // Destroy(gameObject, 1.4f);
            Invoke(nameof(DisableObject), 1.4f);
            PlayAnimationByTrigger(AnimationType.DEATH);
        }
        private void DisableObject()
        {
            gameObject.SetActive(false);
        }

        public void OnDamage(float f)
        {
            if(flashColor != null)
            {
                flashColor.Flash();
            }

            //transform.position -= transform.forward;            

            _currentLife -= f;
            UpdateLifeIndicatorAnimated();

            if (_currentLife <= 0) 
            {
                Kill();
            }
        }

        #region ANIMATION
        private void UpdateLifeIndicatorAnimated(float duration = 0.1f)
        {
            if (lifeIndicator == null) return;

            float ratio = Mathf.Clamp01(_currentLife / Mathf.Max(0.0001f, startLife));
            float newY = _lifeInitScale.y * ratio;
            float delta = _lifeInitScale.y - newY;

            lifeIndicator.transform.DOScaleY(newY, duration);

            // Reposiciona o centro para manter a base fixa
            float newLocalY = _lifeInitLocalPos.y - (delta * 0.5f);
            lifeIndicator.transform.DOLocalMoveY(newLocalY, duration);

            if (ratio <= 0f)
            {
                lifeIndicator.SetActive(false);
            }                
        }
        private void BornAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger(animationType);
        }

        #endregion        

        public void Damage(float damage)
        {
            //Debug.Log("Dano de: " + damage + " pts. Vida atual: " + _currentLife);
            OnDamage(damage);
        }
        public void Damage(float damage, Vector3 damageDirection)
        {
            //Debug.Log("Dano de: " + damage + " pts. Vida atual: " + _currentLife);
            OnDamage(damage);
            transform.DOMove(transform.position - damageDirection, 0.1f);           

        }

        private void OnCollisionEnter(Collision collision)
        {
            PlayerManager p = collision.transform.GetComponent<PlayerManager>();

            if (p != null) 
            {
                //Debug.Log("Player colidiu com um inimigo");
                p.playerHealth.Damage(1);
            }
        }

    }
}
