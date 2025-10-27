using ESM.Core.Singleton;
using ESM.StateMachine;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using static GameManager;
using System.Collections.Generic;
using NaughtyAttributes;

[DefaultExecutionOrder(-999)]
public class PlayerManager : Singleton<PlayerManager>
{
    public enum PlayerStates
    {
        IDLE,
        WALK,
        RUN,
        JUMP,
        DEATH,
        RECHARGING
    }

    #region PUBLICS
    [Header("Player States")]
    public StateMachine<PlayerStates> stateMachine;

    [Header("Life Setup")]
    public HealthBase_Player playerHealth;
    public string messageDeath = "YOU DIED";
    public GameObject globalVolume;

    [Header("Animation Setup")]
    public Animator animator;
    public CharacterController characterController;

    [Header("Colors to Flash")]
    public List<FlashColor> flashColors;

    [Header("Speed Setup")]
    public float speed = 25f;
    public float turnSpeed = 115f;

    [Header("Jump Setup")]
    public float gravity = 23.0f;
    public float jumpSpeed = 17f;
    public KeyCode jumpKeyCode = KeyCode.Space;

    [Header("Run Setup")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;
    #endregion

    #region PRIVATES
    private float vSpeed = 0f;
    private string _currentStateToShow;
    private int _currentPlayerIsAlive = 0;
    #endregion

    private void OnValidate()
    {
        if (playerHealth == null) playerHealth = GetComponent<HealthBase_Player>();
    }

    protected override void Awake()
    {
        base.Awake();

        OnValidate();
        playerHealth.OnDamage += Damage;
        playerHealth.OnKill += OnKill;
    }

    private void Start()
    {
        InitPlayerStates();
        _currentStateToShow = stateMachine.CurrentState.ToString();
    }

    void Update()
    {
        _currentStateToShow = stateMachine.CurrentState.ToString();
        _currentPlayerIsAlive = PlayerPrefs.GetInt("PlayerIsAlive");

        #region PLAYER MOVMENT
        if (_currentPlayerIsAlive == 1)
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);
            var inputAxisVertical = Input.GetAxis("Vertical");
            var speedVector = transform.forward * inputAxisVertical * speed;

            if (characterController.isGrounded)
            {
                vSpeed = 0;
                if (Input.GetKeyDown(jumpKeyCode))
                {
                    vSpeed = jumpSpeed;
                    if (_currentStateToShow != "PlayerStateJUMP")
                    {
                        stateMachine.SwitchState(PlayerStates.JUMP);
                    }                 
                }
            }

            vSpeed -= gravity * Time.deltaTime;
            speedVector.y = vSpeed;

            var isWalking = inputAxisVertical != 0;
            if (isWalking)
            {
                if (Input.GetKey(keyRun))
                {
                    speedVector *= speedRun;
                    animator.speed = speedRun;
                    if (_currentStateToShow != "PlayerStateRUN")
                    {
                        stateMachine.SwitchState(PlayerStates.RUN);
                    }
                }
                else
                {
                    animator.speed = 1;
                    if (_currentStateToShow != "PlayerStateWALK")
                    {
                        stateMachine.SwitchState(PlayerStates.WALK);
                    }
                }
            } 
            else
            {
                if (_currentStateToShow != "PlayerStateIDLE" && characterController.isGrounded)
                {
                    stateMachine.SwitchState(PlayerStates.IDLE);
                }
            }

            characterController.Move(speedVector * Time.deltaTime);

            if (inputAxisVertical != 0)
            {
                animator.SetBool("Run", true);
            }
            else
            {
                animator.SetBool("Run", false);
            }
        }
        #endregion
    }
    public void InitPlayerStates()
    {
        stateMachine = new StateMachine<PlayerStates>();
        stateMachine.Init();
        stateMachine.RegisterStates(PlayerStates.IDLE, new PlayerStateIDLE());
        stateMachine.RegisterStates(PlayerStates.WALK, new PlayerStateWALK());
        stateMachine.RegisterStates(PlayerStates.RUN, new PlayerStateRUN());
        stateMachine.RegisterStates(PlayerStates.JUMP, new PlayerStateJUMP());
        stateMachine.RegisterStates(PlayerStates.DEATH, new PlayerStateDEATH());
        stateMachine.RegisterStates(PlayerStates.RECHARGING, new PlayerStateRECHARGING());
        
        stateMachine.SwitchState(PlayerStates.IDLE);
    }  

    #region LIFE
    public void OnKill(HealthBase_Player h)
    {
        if(_currentPlayerIsAlive == 1)
        {
            _currentPlayerIsAlive = 0;
            PlayerPrefs.SetInt("PlayerIsAlive", 0);
            animator.SetTrigger("Death");
            stateMachine.SwitchState(PlayerStates.DEATH);

            globalVolume.SetActive(true);
            Invoke(nameof(ShowDiedMessage), 1.0f);
            Invoke(nameof(RespawnPosition), 4.0f);
            Invoke(nameof(ReactivatePlayer), 4.1f);
            Invoke(nameof(ChangeScreenToNormal), 4.0f);
        }
    }
    private void ChangeScreenToNormal()
    {
        globalVolume.SetActive(false);
    }

    private void ShowDiedMessage()
    {
        if (messageDeath != "")
        {
            var messageManagerTemp = MessageManager.Instance;
            if (messageManagerTemp != null)
            {
                messageManagerTemp.ChangeMessageTemporary(messageDeath, 1.5f);
            }
        }
    }
    private void ReactivatePlayer()
    {
        playerHealth.ResetLife();
        animator.SetTrigger("Revive");
        stateMachine.SwitchState(PlayerStates.IDLE);        
    }

    [NaughtyAttributes.Button]
    public void RespawnPosition()
    {
        if (CheckPointManager.Instance.HasCheckPoint())
        {
            transform.position = CheckPointManager.Instance.GetPositionFromLastCheckPoint();
        }        
    }
    public void Damage(HealthBase_Player h)
    {
        if (CameraShakeManager.Instance != null)
        {
            CameraShakeManager.Instance.ShakeON();
        }

        flashColors.ForEach(i => i.Flash());
        
    }

    public void Damage(float damage, Vector3 direction)
    {
        if (CameraShakeManager.Instance != null)
        {
            CameraShakeManager.Instance.ShakeON();
        }

        flashColors.ForEach(i => i.Flash());
    }
    #endregion

}
