using UnityEngine;
using TradeMarket.ItemSystem;

namespace TradeMarket.PlayerSystem
{
    public class PlayerController
    {
        private PlayerModel playerModel;
        private PlayerStateMachine playerStateMachine;

        public PlayerModel PlayerModel => playerModel;

        public PlayerController(PlayerModel modelToSet)
        {
            playerModel = modelToSet;
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

        private void StopPlayerMovement()
        {
            playerModel.SetMovement(Vector2.zero);
            playerModel.SetIsWalking(false);
        }

        public void Update() => playerStateMachine.Update();
    }
}