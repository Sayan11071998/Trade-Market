using TradeMarket.ItemSystem;

namespace TradeMarket.PlayerSystem
{
    public class PlayerService
    {
        public PlayerModel PlayerModel { get; private set; }
        public PlayerController PlayerController { get; private set; }
        public PlayerView PlayerView { get; private set; }

        public PlayerService(PlayerView playerView, PlayerScriptableObject playerScriptableObject, 
                           PlayerDataScriptableObject playerDataSO = null, ItemScriptableObject initialItem = null)
        {
            PlayerView = playerView;
            PlayerModel = new PlayerModel(playerScriptableObject, playerDataSO);
            PlayerController = new PlayerController(PlayerModel);
            PlayerView.SetController(PlayerController);

            // Only set initial item if no persistent data exists or if persistent data doesn't have an item
            if (initialItem != null && (playerDataSO == null || !playerDataSO.hasInitializedThisSession || playerDataSO.currentItem == null))
            {
                PlayerModel.SetItem(initialItem);
            }
        }

        // Method to save current state before scene transition
        public void SavePlayerState()
        {
            PlayerModel.SaveState();
        }
    }
}