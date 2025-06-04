using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 Movement;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private Rigidbody2D playerRigidBody;
    private Animator animator;

    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        moveAction = playerInput.actions["Move"];
    }

    private void Update()
    {
        Movement = moveAction.ReadValue<Vector2>();

        Movement.Set(Movement.x, Movement.y);
        playerRigidBody.linearVelocity = Movement * moveSpeed;

        animator.SetFloat(horizontal, Movement.x);
        animator.SetFloat(vertical, Movement.y);

        if (Movement != Vector2.zero)
        {
            animator.SetFloat(lastHorizontal, Movement.x);
            animator.SetFloat(lastVertical, Movement.y);
        }
    }
}