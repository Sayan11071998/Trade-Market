using UnityEngine;
using UnityEngine.InputSystem;
using TradeMarket.Utilities;

namespace TradeMarket.PlayerSystem
{
    public class PlayerView : MonoBehaviour
    {
        private PlayerController playerController;
        private PlayerInput playerInput;
        private InputAction moveAction;
        private Rigidbody2D playerRigidBody;
        private Animator playerAnimator;

        public void SetController(PlayerController controllerToSet) => playerController = controllerToSet;

        private void Awake()
        {
            playerRigidBody = GetComponent<Rigidbody2D>();
            playerAnimator = GetComponent<Animator>();
            playerInput = GetComponent<PlayerInput>();

            moveAction = playerInput.actions[GameString.PlayerInputActionMove];
        }

        private void Update()
        {
            if (playerController != null)
            {
                HandleInput();
                playerController.Update();
                UpdatePhysics();
                UpdateAnimator();
            }
        }

        private void HandleInput()
        {
            Vector2 movement = moveAction.ReadValue<Vector2>();
            playerController.SetMovement(movement);
        }

        private void UpdatePhysics() => playerRigidBody.linearVelocity = playerController.PlayerModel.PlayerMovementVelocity;

        private void UpdateAnimator()
        {
            Vector2 movement = playerController.PlayerModel.Movement;
            Vector2 lastMovement = playerController.PlayerModel.LastMovement;

            playerAnimator.SetFloat(GameString.PlayerAnimationFloatHorizontal, movement.x);
            playerAnimator.SetFloat(GameString.PlayerAnimationFloatVertical, movement.y);
            playerAnimator.SetFloat(GameString.PlayerAnimationFloatLastHorizontal, lastMovement.x);
            playerAnimator.SetFloat(GameString.PlayerAnimationFloatLastVertical, lastMovement.y);
        }
    }
}