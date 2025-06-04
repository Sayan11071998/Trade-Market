using UnityEngine;

namespace TradeMarket.PlayerSystem
{
    public class PlayerController
    {
        private PlayerModel model;

        public PlayerController(PlayerModel model)
        {
            this.model = model;
        }

        public void SetMovement(Vector2 movement)
        {
            model.SetMovement(movement);
        }
    }
}