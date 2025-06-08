using UnityEngine;
using TradeMarket.ItemSystem;

namespace TradeMarket.PlayerSystem
{
    public class PlayerController
    {
        private PlayerModel playerModel;
        private PlayerView playerView;
        private PlayerStateMachine playerStateMachine;

        public PlayerModel PlayerModel => playerModel;
        public PlayerView PlayerView => playerView;

        public PlayerController(PlayerModel modelToSet, PlayerView playerViewToSet)
        {
            playerModel = modelToSet;
            playerView = playerViewToSet;
            playerStateMachine = new PlayerStateMachine(this);
        }

        public void SetMovement(Vector2 movement)
        {
            if (playerModel.IsInTradeMode)
            {
                StopPlayerMovement();
                return;
            }

            bool isWalking = movement.magnitude > 0.1f;

            playerModel.SetMovement(movement);
            playerModel.SetIsWalking(isWalking);

            if (movement != Vector2.zero)
                playerModel.SetLastMovement(movement);
        }

        public void ToggleInventory()
        {
            bool newInventoryState = !playerModel.IsInventoryOpen;
            playerModel.SetInventoryOpen(newInventoryState);
        }

        public void SetTradeMode(bool isInTradeMode)
        {
            if (isInTradeMode)
                StopPlayerMovement();

            playerModel.SetTradeMode(isInTradeMode);
        }

        public void SetCurrentItem(ItemScriptableObject item) => playerModel.SetItem(item);

        public Vector2 GetPlayerVelocity() => playerModel.GetMovementVelocity();

        public void StopPlayerMovement()
        {
            playerModel.SetMovement(Vector2.zero);
            playerModel.SetIsWalking(false);
        }

        public void DisableControls() => playerModel.SetControlsEnabled(false);

        public void EnableControls() => playerModel.SetControlsEnabled(true);

        public void EnableFire() => playerModel.EnableFire(true);

        public void DisableFire() => playerModel.EnableFire(false);

        public bool TryFire(out Vector2 fireDirection)
        {
            fireDirection = Vector2.zero;

            if (!playerModel.CanFireNow())
                return false;

            fireDirection = playerModel.LastMovement.normalized;

            if (fireDirection == Vector2.zero)
                fireDirection = Vector2.right;

            playerModel.RegisterFire();
            return true;
        }

        public void TakeDamage(float damageValue) => PlayerModel.TakeDamage(damageValue);

        public void HandleDeath()
        {
            if (PlayerModel.IsDead && playerStateMachine.CurrentStateEnum != PlayerState.Dead)
                playerStateMachine.ChangeState(PlayerState.Dead);
        }

        public void Update() => playerStateMachine.Update();
    }
}