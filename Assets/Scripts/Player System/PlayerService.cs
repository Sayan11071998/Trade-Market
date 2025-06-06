using TradeMarket.ItemSystem;
using TradeMarket.SaveSystem;

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

            if (initialItem != null
                            && (playerDataSO == null || !playerDataSO.hasInitializedThisSession || playerDataSO.currentItem == null))
                PlayerController.SetCurrentItem(initialItem);
        }

        public void SavePlayerState() => PlayerModel.SaveState();
    }
}