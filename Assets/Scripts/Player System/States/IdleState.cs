using UnityEngine;
using TradeMarket.Utilities;

namespace TradeMarket.PlayerSystem
{
    public class IdleState : IState
    {
        private PlayerController playerController;
        private PlayerStateMachine playerStateMachine;

        public IdleState(PlayerController controllerToSet, PlayerStateMachine stateMachineToSet)
        {
            playerController = controllerToSet;
            playerStateMachine = stateMachineToSet;
        }

        public void OnStateEnter() { }

        public void Update()
        {
            if (playerController?.PlayerModel == null) return;

            if (playerController.PlayerModel.IsDead)
            {
                playerStateMachine.ChangeState(PlayerState.Dead);
                return;
            }

            Vector2 movement = playerController.PlayerModel.Movement;

            if (movement.magnitude > 0.1f)
                playerStateMachine.ChangeState(PlayerState.Walk);
        }

        public void OnStateExit() { }
    }
}