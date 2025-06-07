using TradeMarket.ItemSystem;

namespace TradeMarket.UISystem
{
    public class UIService
    {
        public UIView UIView { get; private set; }
        public UIController UIController { get; private set; }

        public UIService(UIView uiView)
        {
            UIView = uiView;
            UIController = new UIController(uiView);
        }

        public void ToggleInventoryPanel() => UIController.ToggleInventoryPanel();

        public void ShowTradeConfirmation(string npcName, ItemScriptableObject playerItem, ItemScriptableObject npcItem)
            => UIController.ShowTradeConfirmation(npcName, playerItem, npcItem);

        public void ShowGameCompletion() => UIController.ShowGameCompletion();
    }
}