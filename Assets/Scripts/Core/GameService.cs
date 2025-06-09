using UnityEngine;
using System.Collections.Generic;
using TradeMarket.PlayerSystem;
using TradeMarket.ItemSystem;
using TradeMarket.Utilities;
using TradeMarket.UISystem;
using TradeMarket.NPCSystem;
using TradeMarket.SaveSystem;
using TradeMarket.EnemySystem;

namespace TradeMarket.Core
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        public PlayerService playerService { get; private set; }
        public NPCManager npcManager { get; private set; }
        public EnemyManager enemyManager { get; private set; }
        public UIService uiService { get; private set; }

        [Header("Player")]
        [SerializeField] private PlayerView playerView;
        [SerializeField] private PlayerScriptableObject playerScriptableObject;
        [SerializeField] private PlayerDataScriptableObject playerDataScriptableObject;

        [Header("Initial Item")]
        [SerializeField] private ItemScriptableObject initialPlayerItem;

        [Header("NPCs")]
        [SerializeField] private List<NPCSetup> npcSetups = new List<NPCSetup>();

        [Header("Enemies")]
        [SerializeField] private List<EnemySetup> enemySetups = new List<EnemySetup>();

        [Header("UI")]
        [SerializeField] private UIView uiView;

        private PlayerSaveManager saveManager;

        protected override void Awake()
        {
            base.Awake();
            InitializeServices();
        }

        private void InitializeServices()
        {
            saveManager = new PlayerSaveManager(playerDataScriptableObject);
            playerService = new PlayerService(playerView, playerScriptableObject, playerDataScriptableObject, initialPlayerItem);
            npcManager = new NPCManager(npcSetups);
            enemyManager = new EnemyManager(enemySetups);
            uiService = new UIService(uiView);

            SubscribeToEvents();
        }

        private void SubscribeToEvents() => playerService.PlayerModel.OnInventoryToggled += uiService.ToggleInventoryPanel;

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
            SavePlayerStateOnDestroy();
        }

        private void UnsubscribeFromEvents()
        {
            if (playerService?.PlayerModel != null)
                playerService.PlayerModel.OnInventoryToggled -= uiService.ToggleInventoryPanel;
        }

        private void SavePlayerStateOnDestroy() => playerService?.SavePlayerState();

        public void SaveGameState() => playerService?.SavePlayerState();

        public void ResetGameData() => saveManager?.ResetData();

        public bool ExecuteTradeWithNPC(string npcName)
        {
            NPCController npc = npcManager.GetNPCByName(npcName);
            if (npc == null) return false;

            ItemScriptableObject playerItem = playerService.PlayerModel.CurrentItem;
            ItemScriptableObject receivedItem = npc.ExecuteTrade(playerItem);

            if (receivedItem != null)
            {
                playerService.PlayerModel.SetItem(receivedItem);
                return true;
            }

            return false;
        }
    }
}