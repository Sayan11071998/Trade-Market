using TradeMarket.Core;
using TradeMarket.PlayerSystem;

namespace TradeMarket.UISystem
{
    public class UIService
    {
        public UIView UIView { get; private set; }

        public UIService(UIView uiViewToSet)
        {
            UIView = uiViewToSet;

            UIView.SetService(this);
            InitializeUI();
        }

        private void InitializeUI()
        {
            bool isInventoryOpen = GameService.Instance.playerService.PlayerModel.IsInventoryOpen;
            UIView.SetInventoryPanelActive(isInventoryOpen);

            UpdateInventoryDisplay();
        }

        public void ToggleInventoryPanel()
        {
            bool isInventoryOpen = GameService.Instance.playerService.PlayerModel.IsInventoryOpen;
            UIView.SetInventoryPanelActive(isInventoryOpen);

            if (isInventoryOpen)
                UpdateInventoryDisplay();
        }

        private void UpdateInventoryDisplay()
        {
            var currentItem = GameService.Instance.playerService.PlayerModel.CurrentItem;
            UIView.UpdateInventoryItem(currentItem);
        }
    }
}