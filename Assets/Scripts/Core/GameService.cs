using UnityEngine;
using System.Collections.Generic;
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
        public NPCManager npcManager { get; private set; }
        public UIService uiService { get; private set; }

        [Header("Player")]
        [SerializeField] private PlayerView playerView;
        [SerializeField] private PlayerScriptableObject playerScriptableObject;
        [SerializeField] private PlayerDataScriptableObject playerDataScriptableObject; // Add this

        [Header("Initial Item")]
        [SerializeField] private ItemScriptableObject initialPlayerItem;

        [Header("NPCs")]
        [SerializeField] private List<NPCSetup> npcSetups = new List<NPCSetup>();

        [Header("UI")]
        [SerializeField] private UIView uiView;

        protected override void Awake()
        {
            base.Awake();
            InitializeServices();
        }

        private void InitializeServices()
        {
            // Pass the persistent data ScriptableObject to PlayerService
            playerService = new PlayerService(playerView, playerScriptableObject, playerDataScriptableObject, initialPlayerItem);
            npcManager = new NPCManager(npcSetups);
            uiService = new UIService(uiView);

            playerService.PlayerModel.OnInventoryToggled += uiService.ToggleInventoryPanel;
        }

        private void OnDestroy()
        {
            if (playerService?.PlayerModel != null)
            {
                playerService.PlayerModel.OnInventoryToggled -= uiService.ToggleInventoryPanel;
                // Save state before destruction
                playerService.SavePlayerState();
            }
        }

        // Method to save game state (call this before scene transitions)
        public void SaveGameState()
        {
            playerService?.SavePlayerState();
        }

        // Method to reset game data (for new game)
        public void ResetGameData()
        {
            if (playerDataScriptableObject != null)
                playerDataScriptableObject.ResetData();
        }

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