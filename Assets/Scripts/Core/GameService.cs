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

        [Header("Initial Item")]
        [SerializeField] private ItemScriptableObject initialPlayerItem;

        [Header("NPCs")]
        [SerializeField] private List<NPCSetup> npcSetups = new List<NPCSetup>();

        [Header("UI")]
        [SerializeField] private UIView uiView;

        protected override void Awake()
        {
            base.Awake();
            ValidateReferences();
            InitializeServices();
        }

        private void ValidateReferences()
        {
            if (playerView == null) Debug.LogError("PlayerView is not assigned!");
            if (playerScriptableObject == null) Debug.LogError("PlayerScriptableObject is not assigned!");
            if (uiView == null) Debug.LogError("UIView is not assigned!");
            if (npcSetups.Count == 0) Debug.LogWarning("No NPCs have been set up!");

            foreach (var npcSetup in npcSetups)
            {
                if (npcSetup.npcView == null) Debug.LogError("NPC View is missing in setup!");
                if (npcSetup.npcData == null) Debug.LogError("NPC Data is missing in setup!");
            }
        }

        private void InitializeServices()
        {
            // Initialize Player Service
            playerService = new PlayerService(playerView, playerScriptableObject, initialPlayerItem);

            // Initialize NPC Manager
            npcManager = new NPCManager(npcSetups);

            // Initialize UI Service
            uiService = new UIService(uiView);

            // Set up event connections
            playerService.PlayerModel.OnInventoryToggled += uiService.ToggleInventoryPanel;

            Debug.Log($"All services initialized successfully! {npcSetups.Count} NPCs loaded.");
        }

        private void OnDestroy()
        {
            if (playerService?.PlayerModel != null)
                playerService.PlayerModel.OnInventoryToggled -= uiService.ToggleInventoryPanel;
        }

        // Helper methods for NPC system
        public NPCService GetNPCByName(string npcName)
        {
            return npcManager.GetNPCByName(npcName);
        }

        public List<NPCService> GetAllNPCs()
        {
            return npcManager.GetAllNPCs();
        }

        public int GetNPCCount()
        {
            return npcManager.GetNPCCount();
        }
    }
}