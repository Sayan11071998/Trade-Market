using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using TradeMarket.Utilities;
using TradeMarket.BulletSystem;

namespace TradeMarket.PlayerSystem
{
    public class PlayerView : MonoBehaviour
    {
        [Header("Dialogue")]
        [SerializeField] private GameObject dialoguePanel;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private float dialogueDisplayTime = 3f;

        [Header("Shooting")]
        [SerializeField] private BulletView playerBulletPrefab;
        [SerializeField] private BulletScriptableObject playerBulletData;
        [SerializeField] private Transform firePoint;
        [SerializeField] private Transform bulletPoolParent;

        private PlayerController playerController;
        private BulletService playerBulletService;
        private PlayerInput playerInput;
        private InputAction moveAction;
        private InputAction inventoryAction;
        private InputAction fireAction;
        private Rigidbody2D playerRigidBody;
        private Animator playerAnimator;
        private Coroutine dialogueCoroutine;

        public void SetController(PlayerController controllerToSet) => playerController = controllerToSet;

        private void Awake()
        {
            playerRigidBody = GetComponent<Rigidbody2D>();
            playerAnimator = GetComponent<Animator>();
            playerInput = GetComponent<PlayerInput>();

            moveAction = playerInput.actions[GameString.PlayerInputActionMove];
            inventoryAction = playerInput.actions[GameString.PlayerInputActionInventory];
            fireAction = playerInput.actions[GameString.PlayerInputActionFire];
            playerBulletService = new BulletService(playerBulletPrefab, playerBulletData, bulletPoolParent);
            dialoguePanel?.SetActive(false);
        }

        private void Update()
        {
            if (playerController != null)
            {
                HandleInput();
                playerController.Update();
                UpdatePhysics();
                UpdateAnimator();
                playerBulletService.UpdateBullets();
            }
        }

        private void HandleInput()
        {
            if (!playerController.PlayerModel.AreControlsEnabled) return;

            HandleMoveInput();
            HandleInventoryInput();
            HandleFireInput();
        }

        private void HandleMoveInput()
        {
            Vector2 movement = moveAction.ReadValue<Vector2>();
            playerController.SetMovement(movement);
        }

        private void HandleInventoryInput()
        {
            if (inventoryAction.WasPressedThisFrame())
                playerController.ToggleInventory();
        }

        private void HandleFireInput()
        {
            if (fireAction.WasPressedThisFrame() && playerController.PlayerModel.CanFire)
                FireBullet();
        }

        private void UpdatePhysics() => playerRigidBody.linearVelocity = playerController.GetPlayerVelocity();

        private void UpdateAnimator()
        {
            Vector2 movement = playerController.PlayerModel.Movement;
            Vector2 lastMovement = playerController.PlayerModel.LastMovement;
            bool isWalking = playerController.PlayerModel.IsWalking;

            playerAnimator.SetFloat(GameString.PlayerAnimationFloatHorizontal, movement.x);
            playerAnimator.SetFloat(GameString.PlayerAnimationFloatVertical, movement.y);
            playerAnimator.SetFloat(GameString.PlayerAnimationFloatLastHorizontal, lastMovement.x);
            playerAnimator.SetFloat(GameString.PlayerAnimationFloatLastVertical, lastMovement.y);
            playerAnimator.SetBool(GameString.PlayerAnimationBoolIsWalking, isWalking);
        }

        public void ActivateDialoguePanel() => dialoguePanel?.SetActive(true);

        public void ShowDialogue(string dialogue)
        {
            if (dialoguePanel == null || dialogueText == null) return;

            if (dialogueCoroutine != null)
                StopCoroutine(dialogueCoroutine);

            dialogueText.text = dialogue;
            dialoguePanel.SetActive(true);
            dialogueCoroutine = StartCoroutine(HideDialogueAfterDelay());
        }

        private System.Collections.IEnumerator HideDialogueAfterDelay()
        {
            yield return new WaitForSeconds(dialogueDisplayTime);
            dialoguePanel?.SetActive(false);
        }

        private void FireBullet()
        {
            Vector2 fireDirection = playerController.PlayerModel.LastMovement.normalized;

            if (fireDirection == Vector2.zero)
                fireDirection = Vector2.right;

            playerBulletService.FireBullet(firePoint.position, fireDirection);
        }
    }
}