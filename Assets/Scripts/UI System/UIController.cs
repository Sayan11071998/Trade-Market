using TradeMarket.Core;
using TradeMarket.ItemSystem;
using UnityEngine.SceneManagement;
using TradeMarket.Utilities;

namespace TradeMarket.UISystem
{
    public class UIController
    {
        private UIView uiView;

        public UIController(UIView uiViewToSet)
        {
            uiView = uiViewToSet;
            InitializeController();
        }

        private void InitializeController()
        {
            uiView.SetController(this);
            RefreshInventoryUI();
        }

        public void ToggleInventoryPanel() => RefreshInventoryUI();

        private void RefreshInventoryUI()
        {
            bool isInventoryOpen = GameService.Instance.playerService.PlayerModel.IsInventoryOpen;
            uiView.SetInventoryPanelActive(isInventoryOpen);

            if (isInventoryOpen)
                UpdateInventoryDisplay();
        }

        private void UpdateInventoryDisplay()
        {
            var currentItem = GameService.Instance.playerService.PlayerModel.CurrentItem;
            uiView.UpdateInventoryItem(currentItem);
        }

        public void ShowTradeConfirmation(string npcName, ItemScriptableObject playerItem, ItemScriptableObject npcItem)
        {
            GameService.Instance.playerService.PlayerController.SetTradeMode(true);
            uiView.ShowTradeConfirmationPanel(npcName, playerItem, npcItem);
        }

        public void OnTradeConfirmed(string npcName)
        {
            bool tradeSuccess = GameService.Instance.ExecuteTradeWithNPC(npcName);

            if (tradeSuccess)
            {
                HideTradeConfirmation();
                SetPlayerTradeMode(false);
                RefreshInventoryIfOpen();
            }
        }

        public void OnTradeCancelled()
        {
            HideTradeConfirmation();
            SetPlayerTradeMode(false);
        }

        public void ShowGameCompletion() => uiView.ShowGameUIPanel();

        public void RestartGame()
        {
            ResetGameData();
            LoadVillageScene();
        }

        public void QuitGame()
        {
            ResetGameData();
            QuitApplication();
        }

        private void HideTradeConfirmation() => uiView.HideTradeConfirmationPanel();

        private void SetPlayerTradeMode(bool isTrading) => GameService.Instance.playerService.PlayerController.SetTradeMode(isTrading);

        private void RefreshInventoryIfOpen()
        {
            if (GameService.Instance.playerService.PlayerModel.IsInventoryOpen)
                UpdateInventoryDisplay();
        }

        private void ResetGameData() => GameService.Instance.ResetGameData();

        private void LoadVillageScene() => SceneManager.LoadScene(GameString.VillageScene);

        private void QuitApplication() => UnityEngine.Application.Quit();
    }
}