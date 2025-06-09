using System.Collections.Generic;
using TradeMarket.Utilities;

namespace TradeMarket.PlayerSystem
{
    public class PlayerStateMachine
    {
        private Dictionary<PlayerState, IState> states;
        private IState currentState;
        private PlayerState currentPlayerState;
        private PlayerController playerController;

        public PlayerStateMachine(PlayerController controllerToSet)
        {
            playerController = controllerToSet;
            currentPlayerState = (PlayerState)(-1);
            InitializeStates();
            ChangeState(PlayerState.Idle);
        }

        private void InitializeStates()
        {
            states = new Dictionary<PlayerState, IState>()
            {
                { PlayerState.Idle, new IdleState(playerController, this) },
                { PlayerState.Walk, new WalkState(playerController, this) },
                { PlayerState.Dead, new DeadState(playerController, this) },
            };
        }

        public void ChangeState(PlayerState newState)
        {
            if (currentPlayerState == newState) return;

            if (states.ContainsKey(newState))
            {
                currentState?.OnStateExit();
                currentPlayerState = newState;
                currentState = states[newState];
                currentState.OnStateEnter();
            }
        }

        public void Update() => currentState?.Update();
    }
}