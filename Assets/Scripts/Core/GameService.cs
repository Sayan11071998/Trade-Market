using TradeMarket.PlayerSystem;
using TradeMarket.ItemSystem;
using TradeMarket.Utilities;
using UnityEngine;
using TradeMarket.UISystem;

namespace TradeMarket.Core
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        public PlayerService playerService { get; private set; }
        public UIService uiService { get; private set; }

        [Header("Player")]
        [SerializeField] private PlayerView playerView;
        [SerializeField] private PlayerScriptableObject playerScriptableObject;

        [Header("Initial Item")]
        [SerializeField] private ItemScriptableObject initialPlayerItem;

        [Header("UI")]
        [SerializeField] private UIView uiView;

        protected override void Awake()
        {
            base.Awake();
            InitializeServices();
        }

        private void InitializeServices()
        {
            playerService = new PlayerService(playerView, playerScriptableObject, initialPlayerItem);
            uiService = new UIService(uiView, playerService);

            playerService.PlayerModel.OnInventoryToggled += uiService.ToggleInventoryPanel;
        }

        private void OnDestroy()
        {
            if (playerService?.PlayerModel != null)
                playerService.PlayerModel.OnInventoryToggled -= uiService.ToggleInventoryPanel;
        }
    }
}