using UnityEngine;
using TradeMarket.Utilities;
using TradeMarket.SoundSystem;

namespace TradeMarket.PlayerSystem
{
    public class WalkState : IState
    {
        private PlayerController playerController;
        private PlayerStateMachine playerStateMachine;

        public WalkState(PlayerController controllerToSet, PlayerStateMachine stateMachineToSet)
        {
            playerController = controllerToSet;
            playerStateMachine = stateMachineToSet;
        }

        public void OnStateEnter() => SoundManager.Instance.soundService.PlaySoundEffects(SoundType.PlayerWalk, true);

        public void Update()
        {
            if (playerController?.PlayerModel == null) return;

            if (playerController.PlayerModel.IsDead)
            {
                playerStateMachine.ChangeState(PlayerState.Dead);
                return;
            }

            Vector2 movement = playerController.PlayerModel.Movement;

            if (movement.magnitude <= 0.1f)
                playerStateMachine.ChangeState(PlayerState.Idle);
        }

        public void OnStateExit() => SoundManager.Instance.soundService.StopSoundEffects();
    }
}