using TradeMarket.PlayerSystem;

namespace TradeMarket.UISystem
{
    public class UIService
    {
        public UIView UIView { get; private set; }
        private PlayerService playerService;

        public UIService(UIView uiView, PlayerService playerServiceToSet)
        {
            UIView = uiView;
            playerService = playerServiceToSet;

            UIView.SetService(this);
            InitializeUI();
        }

        private void InitializeUI()
        {
            bool isInventoryOpen = playerService.PlayerModel.IsInventoryOpen;
            UIView.SetInventoryPanelActive(isInventoryOpen);

            UpdateInventoryDisplay();
        }

        public void ToggleInventoryPanel()
        {
            bool isInventoryOpen = playerService.PlayerModel.IsInventoryOpen;
            UIView.SetInventoryPanelActive(isInventoryOpen);

            if (isInventoryOpen)
                UpdateInventoryDisplay();
        }

        private void UpdateInventoryDisplay()
        {
            var currentItem = playerService.PlayerModel.CurrentItem;
            UIView.UpdateInventoryItem(currentItem);
        }
    }
}