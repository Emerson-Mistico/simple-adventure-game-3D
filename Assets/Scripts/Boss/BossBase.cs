using Animation;
using DG.Tweening;
using ESM.StateMachine;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Boss
{

    public enum BossAction
    {
        INIT,
        IDLE,
        WALK,
        ATTACK,
        DEATH
    }

    public class BossBase : MonoBehaviour
    {
        [Header("Boss")]
        public HealthBase healthBase;
        public Collider colliderBoss;

        public GameObject uiBoss;

        [Header("Animation")]
        public float startAnimationDuration = 3f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;
        public ParticleSystem particlesToKill;
        public bool lookAtTarget = false;

        public Animator bossAnimator;
        public List<AnimationSetup> bossAnimationSetups;

        [Header("Movment")]
        public float speed = 5f;
        public List<Transform> waypoints;
        
        [Header("Attack")]
        public int attackAmount = 5;
        public float timeBetweenAttacks = .5f;
        public GunBase gunBase;

        private StateMachine<BossAction> stateMachine;
        private PlayerManager _player;
        private int _playerIsAlive;

        private void Awake()
        {
            Init();
            healthBase.OnKill += OnBossKill;
            uiBoss.SetActive(true);
        }
        private void Start()
        {
            // _player = GameObject.FindObjectOfType<PlayerManager>();
            _player = GameObject.FindAnyObjectByType<PlayerManager>();
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
                healthBase._currentLife = healthBase.startLife;
                healthBase.uiBossUpdater.UpdateValue(healthBase.startLife, healthBase._currentLife);
            }

        }
        private void Init()
        {
            stateMachine = new StateMachine<BossAction>();
            stateMachine.Init();

            stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
            stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());
            stateMachine.RegisterStates(BossAction.DEATH, new BossStateDeath());

            SwtichState(BossAction.INIT);
        }

        public bool IsAlive()
        {
            return healthBase._currentLife > 0;
        }

        #region DEBUG
        [NaughtyAttributes.Button]
        private void SwitchInit()
        {
            SwtichState(BossAction.INIT);
        }
        [NaughtyAttributes.Button]
        private void SwitchWalk()
        {
            SwtichState(BossAction.WALK);
        }
        [NaughtyAttributes.Button]
        private void SwitchAttack()
        {
            SwtichState(BossAction.ATTACK);
        }
        #endregion

        private void OnBossKill(HealthBase h)
        {
            if (colliderBoss != null)
            {
                colliderBoss.enabled = false;
            }
            if (particlesToKill != null)
            {
                particlesToKill.Emit(15);
            }
            PlayAnimationByTrigger(AnimationType.DEATH);
            SwtichState(BossAction.DEATH);
            uiBoss.SetActive(false);
            PlayerPrefs.SetInt("BossFightState", 0);
            PlayerPrefs.SetInt("Boss01Alive", 0);
        }

        #region STATEMACHINE
        public void SwtichState(BossAction state)
        {
            stateMachine.SwitchState(state, this);
        }
        #endregion

        #region ANIMATION
        public void StartInitAnimation()
        {
            if (startWithBornAnimation)
            {
                transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
            }            
        }
        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            var setup = bossAnimationSetups.Find(i => i.anymationType == animationType);
            if (setup != null)
            {
                bossAnimator.SetTrigger(setup.trigger);
            }
        }

        #endregion

        #region ATTACK
        public void StartAttack(Action endCallback = null)
        {
            StartCoroutine(AttackCoroutine(endCallback));
        }

        IEnumerator AttackCoroutine(Action endCallback = null)
        {
            int attacks = 0;
            gunBase.StartShoot();
            while (attacks < attackAmount)
            {
                attacks++;
                PlayAnimationByTrigger(AnimationType.ATTACK);
                // transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo);
                yield return new WaitForSeconds(timeBetweenAttacks);
            }
            gunBase.StopShoot();
            // if (endCallback != null) onArrive.Invoke();
            endCallback?.Invoke(); // Essa linha é o mesmo que o de cima. Apenas outra forma.
        }
        #endregion

        #region MOVMENT      
        public void GoToRandomPoint(Action onArrive = null)
        {
            StartCoroutine(GoToPointCoroutine(waypoints[UnityEngine.Random.Range(0,waypoints.Count)], onArrive));
        }

        IEnumerator GoToPointCoroutine(Transform t, Action onArrive = null)
        {
            PlayAnimationByTrigger(AnimationType.RUN);
            while (Vector3.Distance(transform.position, t.position) > 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }

            // if (onArrive != null) onArrive.Invoke();
            onArrive?.Invoke(); // Essa linha é o mesmo que o de cima. Apenas outra forma.

        }
        #endregion
    }
}

