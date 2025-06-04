using UnityEngine;

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

        public void SetMovement(Vector2 movement) => playerModel.SetMovement(movement);

        public void Update() => playerStateMachine.Update();
    }
}