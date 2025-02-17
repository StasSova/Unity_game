using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class CharacterScript : MonoBehaviour
{
    private Animator animator;
    private CharacterController characterController;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction sprintAction;

    [SerializeField]
    private float moveSpeed = 1.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private bool groundedPlayer;
    private Vector3 playerVelocity;
    private MoveState prevMoveState;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        sprintAction = InputSystem.actions.FindAction("Sprint");
        prevMoveState = MoveState.Idle;
    }

    void Update()
    {
        MoveState moveState = MoveState.Idle;
        groundedPlayer = characterController.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        float sprintValue = sprintAction.ReadValue<float>();

        Vector3 moveForward = Camera.main.transform.forward;
        moveForward.y = 0.0f;
        if (moveForward != Vector3.zero)
        {
            moveForward.Normalize();
        }

        Vector3 moveStep = Time.deltaTime * moveSpeed * (1+ sprintValue) *  (
            moveValue.x * Camera.main.transform.right + 
            moveValue.y * moveForward
        );

        if (moveStep.magnitude != 0) 
        {
            this.transform.forward = moveForward;
            moveState = Mathf.Abs(moveValue.x) > Mathf.Abs(moveValue.y)
                ? (sprintValue > 0 ? MoveState.Run : MoveState.Sidewalk)
                : (sprintValue > 0 ? MoveState.Run : MoveState.Walk);
        }
        characterController.Move(moveStep);
        

        // Makes the player jump
        if (jumpAction.ReadValue<float>() > 0f && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);

        if(moveState != prevMoveState)
        {
            animator.SetInteger("MoveState", (int)moveState);
            prevMoveState = moveState;
        }
    }
}

enum MoveState
{
    Idle = 1,
    Walk = 2, 
    Sidewalk = 3,
    Run = 4,
}
