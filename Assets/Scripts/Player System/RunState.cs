using UnityEngine;
using TradeMarket.Utilities;

namespace TradeMarket.PlayerSystem
{
    public class RunState : IState
    {
        private PlayerController playerController;
        private PlayerStateMachine playerStateMachine;

        public RunState(PlayerController controllerToSet, PlayerStateMachine stateMachineToSet)
        {
            playerController = controllerToSet;
            playerStateMachine = stateMachineToSet;
        }

        public void OnStateEnter() { }

        public void Update()
        {
            Vector2 movement = playerController.PlayerModel.Movement;

            if (movement.magnitude <= 0.1f)
                playerStateMachine.ChangeState(PlayerState.Idle);
        }

        public void OnStateExit() { }
    }
}