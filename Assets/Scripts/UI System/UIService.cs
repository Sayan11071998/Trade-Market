using TradeMarket.Core;
using TradeMarket.ItemSystem;

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

        private void InitializeUI() => RefreshInventoryUI();

        public void ToggleInventoryPanel() => RefreshInventoryUI();

        private void RefreshInventoryUI()
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

        public void ShowTradeConfirmation(string npcName, ItemScriptableObject playerItem, ItemScriptableObject npcItem)
        {
            // Set player movement to be disabled
            GameService.Instance.playerService.PlayerModel.SetTradeUIActive(true);
            UIView.ShowTradeConfirmationPanel(npcName, playerItem, npcItem);
        }

        public void OnTradeConfirmed(string npcName)
        {
            bool tradeSuccess = GameService.Instance.ExecuteTradeWithNPC(npcName);
            
            if (tradeSuccess)
            {
                // Close trade confirmation panel and re-enable movement
                UIView.HideTradeConfirmationPanel();
                GameService.Instance.playerService.PlayerModel.SetTradeUIActive(false);
                
                // Refresh inventory if it's open
                if (GameService.Instance.playerService.PlayerModel.IsInventoryOpen)
                    UpdateInventoryDisplay();
            }
        }

        public void OnTradeCancelled()
        {
            // Close trade confirmation panel and re-enable movement
            UIView.HideTradeConfirmationPanel();
            GameService.Instance.playerService.PlayerModel.SetTradeUIActive(false);
        }
    }
}