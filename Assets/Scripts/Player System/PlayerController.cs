using UnityEngine;

namespace TradeMarket.PlayerSystem
{
    public class PlayerController
    {
        private PlayerModel playerModel;

        public PlayerModel PlayerModel => playerModel;

        public PlayerController(PlayerModel modelToSet) => playerModel = modelToSet;

        public void SetMovement(Vector2 movement) => playerModel.SetMovement(movement);
    }
}