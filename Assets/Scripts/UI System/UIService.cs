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
            GameService.Instance.playerService.PlayerController.SetTradeMode(true);
            UIView.ShowTradeConfirmationPanel(npcName, playerItem, npcItem);
        }

        public void OnTradeConfirmed(string npcName)
        {
            bool tradeSuccess = GameService.Instance.ExecuteTradeWithNPC(npcName);

            if (tradeSuccess)
            {
                UIView.HideTradeConfirmationPanel();
                GameService.Instance.playerService.PlayerController.SetTradeMode(false);

                if (GameService.Instance.playerService.PlayerModel.IsInventoryOpen)
                    UpdateInventoryDisplay();
            }
        }

        public void OnTradeCancelled()
        {
            UIView.HideTradeConfirmationPanel();
            GameService.Instance.playerService.PlayerController.SetTradeMode(false);
        }

        public void ShowGameWonPanel()
        {
            UIView.ShowGameWonPanel();
        }
    }
}