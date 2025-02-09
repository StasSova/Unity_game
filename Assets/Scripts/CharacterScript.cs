using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class CharacterScript : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction jumpAction;
    private CharacterController characterController;

    [SerializeField]
    private float moveSpeed = 1.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private bool groundedPlayer;
    private Vector3 playerVelocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");    
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = characterController.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Vector3 moveForward = Camera.main.transform.forward;
        moveForward.y = 0.0f;
        if (moveForward != Vector3.zero)
        {
            moveForward.Normalize();
        }

        Vector3 moveStep = Time.deltaTime * moveSpeed * (
            moveValue.x * Camera.main.transform.right + 
            moveValue.y * moveForward
        );

        characterController.Move(moveStep);
        

        // Makes the player jump
        if (jumpAction.ReadValue<float>() > 0f && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }
}
