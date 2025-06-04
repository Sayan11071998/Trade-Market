using UnityEngine;
using UnityEngine.InputSystem;

namespace TradeMarket.PlayerSystem
{
    public class PlayerView : MonoBehaviour
    {
        private PlayerModel model;
        private PlayerController controller;
        private PlayerInput playerInput;
        private InputAction moveAction;
        private Rigidbody2D playerRigidBody;
        private Animator animator;

        public void SetModelAndController(PlayerModel model, PlayerController controller)
        {
            this.model = model;
            this.controller = controller;
        }

        private void Awake()
        {
            playerRigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            playerInput = GetComponent<PlayerInput>();
            moveAction = playerInput.actions["Move"];
        }

        private void Update()
        {
            if (controller != null && model != null)
            {
                Vector2 movement = moveAction.ReadValue<Vector2>();
                controller.SetMovement(movement);
                playerRigidBody.linearVelocity = model.Movement * model.MovementSpeed;
                animator.SetFloat("Horizontal", model.Movement.x);
                animator.SetFloat("Vertical", model.Movement.y);
                animator.SetFloat("LastHorizontal", model.LastMovement.x);
                animator.SetFloat("LastVertical", model.LastMovement.y);
            }
        }
    }
}