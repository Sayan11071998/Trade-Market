using UnityEngine;

namespace TradeMarket.PlayerSystem
{
    public class PlayerService
    {
        public PlayerModel PlayerModel { get; private set; }
        public PlayerController PlayerController { get; private set; }
        public PlayerView PlayerView { get; private set; }

        public PlayerService(PlayerView playerView, PlayerScriptableObject playerScriptableObject)
        {
            PlayerView = playerView;
            PlayerModel = new PlayerModel(playerScriptableObject.playerMovementSpeed);
            PlayerController = new PlayerController(PlayerModel);
            PlayerView.SetModelAndController(PlayerModel, PlayerController);
        }
    }
}