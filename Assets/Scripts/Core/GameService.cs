using UnityEngine;
using TradeMarket.PlayerSystem;
using TradeMarket.ItemSystem;
using TradeMarket.Utilities;
using TradeMarket.UISystem;
using TradeMarket.NPCSystem;

namespace TradeMarket.Core
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        public PlayerService playerService { get; private set; }
        public NPCService npcService { get; private set; }
        public UIService uiService { get; private set; }

        [Header("Player")]
        [SerializeField] private PlayerView playerView;
        [SerializeField] private PlayerScriptableObject playerScriptableObject;

        [Header("Initial Item")]
        [SerializeField] private ItemScriptableObject initialPlayerItem;

        [Header("NPC")]
        [SerializeField] private NPCView npcView;
        [SerializeField] private NPCScriptableObject npcScriptableObject;

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
            npcService = new NPCService(npcView, npcScriptableObject);
            uiService = new UIService(uiView);

            playerService.PlayerModel.OnInventoryToggled += uiService.ToggleInventoryPanel;
        }

        private void OnDestroy()
        {
            if (playerService?.PlayerModel != null)
                playerService.PlayerModel.OnInventoryToggled -= uiService.ToggleInventoryPanel;
        }
    }
}