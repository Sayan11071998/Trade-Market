using TradeMarket.Utilities;
using TradeMarket.Core;
using TradeMarket.SoundSystem;

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
            GameService.Instance.soundService.PlaySoundEffects(SoundType.Death);
            GameService.Instance.enemyManager.DisableAllEnemiesFiring();
            GameService.Instance.uiService.ShowGameCompletion(GameString.GameOverText);
        }

        public void Update() { }

        public void OnStateExit() { }
    }
}