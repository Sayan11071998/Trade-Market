using UnityEngine;

namespace TradeMarket.PlayerSystem
{
    public class PlayerController
    {
        private PlayerModel playerModel;

        public PlayerController(PlayerModel modelToSet) => playerModel = modelToSet;

        public void SetMovement(Vector2 movement) => playerModel.SetMovement(movement);

        public Vector2 GetMovement()
        {
            return playerModel.Movement;
        }

        public Vector2 GetLastMovement()
        {
            return playerModel.LastMovement;
        }

        public float GetMovementSpeed()
        {
            return playerModel.MovementSpeed;
        }

        public Vector2 GetVelocity()
        {
            return playerModel.Movement * playerModel.MovementSpeed;
        }
    }
}