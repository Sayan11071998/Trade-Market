using UnityEngine;
using TradeMarket.Utilities;

namespace TradeMarket.PlayerSystem
{
    public class DeadState : IState
    {
        private PlayerController playerController;
        private PlayerStateMachine playerStateMachine;

        public DeadState(PlayerController controllerToSet, PlayerStateMachine stateMachineToSet)
        {
            playerController = controllerToSet;
            playerStateMachine = stateMachineToSet;
        }

        public void OnStateEnter()
        {
            playerController.DisableControls();
            playerController.StopPlayerMovement();
            playerController.DisableFire();
            Debug.Log("Player is Dead!");
        }

        public void Update() { }

        public void OnStateExit() { }
    }
}