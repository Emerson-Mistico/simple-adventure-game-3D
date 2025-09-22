using ESM.Core.Singleton;
using ESM.StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;
using static GameManager;

public class PlayerManager : Singleton<PlayerManager>
{
    public enum PlayerStates
    {
        IDLE,
        WALK,
        RUN,
        JUMP,
        DEATH
    }

    #region PUBLICS
    [Header("Player Setup")]
    public StateMachine<PlayerStates> stateMachine;

    [Header("Animation Setup")]
    public Animator animator;
    public CharacterController characterController;

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

    #endregion

    private void Start()
    {
        InitPlayerStates();
        _currentStateToShow = stateMachine.CurrentState.ToString();
        Debug.Log("Inicial State ->" + _currentStateToShow);
    }

    void Update()
    {
        _currentStateToShow = stateMachine.CurrentState.ToString();

        #region PLAYER MOVMENT

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
                    Debug.Log("Change to: JUMP");
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
                    Debug.Log("Change to: RUN");
                }
            }
            else
            {
                animator.speed = 1;
                if (_currentStateToShow != "PlayerStateWALK")
                {
                    stateMachine.SwitchState(PlayerStates.WALK);
                    Debug.Log("Change to: WALK");
                }
            }
        } 
        else
        {
            if (_currentStateToShow != "PlayerStateIDLE" && characterController.isGrounded)
            {
                stateMachine.SwitchState(PlayerStates.IDLE);
                Debug.Log("Change to: IDLE");
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
        
        stateMachine.SwitchState(PlayerStates.IDLE);
    }  

}
