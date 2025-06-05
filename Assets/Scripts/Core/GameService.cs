using TradeMarket.PlayerSystem;
using TradeMarket.ItemSystem;
using TradeMarket.Utilities;
using UnityEngine;

namespace TradeMarket.Core
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        public PlayerService playerService { get; private set; }

        [Header("Player")]
        [SerializeField] private PlayerView playerView;
        [SerializeField] private PlayerScriptableObject playerScriptableObject;

        [Header("Initial Item")]
        [SerializeField] private ItemScriptableObject initialPlayerItem;

        protected override void Awake()
        {
            base.Awake();
            InitializeServices();
        }

        private void InitializeServices()
        {
            playerService = new PlayerService(playerView, playerScriptableObject, initialPlayerItem);
        }
    }
}