using TradeMarket.PlayerSystem;
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

        protected override void Awake()
        {
            base.Awake();
            InitializeService();
        }

        private void InitializeService()
        {
            playerService = new PlayerService(playerView, playerScriptableObject);
        }
    }
}