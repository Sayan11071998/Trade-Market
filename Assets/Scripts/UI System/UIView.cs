using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TradeMarket.ItemSystem;

namespace TradeMarket.UISystem
{
    public class UIView : MonoBehaviour
    {
        [Header("Inventory UI")]
        [SerializeField] private GameObject inventoryPanel;
        [SerializeField] private Image itemSprite;
        [SerializeField] private TextMeshProUGUI itemNameText;

        [Header("Trade Confirmation UI")]
        [SerializeField] private GameObject tradeConfirmationPanel;
        [SerializeField] private TextMeshProUGUI tradeDescriptionText;
        [SerializeField] private Image playerItemImage;
        [SerializeField] private Image npcItemImage;
        [SerializeField] private TextMeshProUGUI playerItemNameText;
        [SerializeField] private TextMeshProUGUI npcItemNameText;
        [SerializeField] private Button confirmTradeButton;
        [SerializeField] private Button cancelTradeButton;

        private UIService uiService;
        private string currentNPCName;

        public void SetService(UIService uiServiceToSet) => uiService = uiServiceToSet;

        private void Start()
        {
            if (tradeConfirmationPanel != null)
                tradeConfirmationPanel.SetActive(false);

            if (confirmTradeButton != null)
                confirmTradeButton.onClick.AddListener(OnConfirmTradeClicked);

            if (cancelTradeButton != null)
                cancelTradeButton.onClick.AddListener(OnCancelTradeClicked);
        }

        public void SetInventoryPanelActive(bool isActive) => inventoryPanel.SetActive(isActive);

        public void UpdateInventoryItem(ItemScriptableObject item)
        {
            if (item != null)
            {
                itemSprite.sprite = item.ItemIcon;
                itemNameText.text = item.ItemName;

                itemSprite.gameObject.SetActive(true);
                itemNameText.gameObject.SetActive(true);
            }
            else
            {
                itemSprite.gameObject.SetActive(false);
                itemNameText.gameObject.SetActive(false);
            }
        }

        public void ShowTradeConfirmationPanel(string npcName, ItemScriptableObject playerItem, ItemScriptableObject npcItem)
        {
            if (tradeConfirmationPanel == null) return;

            currentNPCName = npcName;

            if (tradeDescriptionText != null)
                tradeDescriptionText.text = $"Trade with {npcName}?";

            if (playerItemImage != null && playerItem != null)
            {
                playerItemImage.sprite = playerItem.ItemIcon;
                playerItemImage.gameObject.SetActive(true);
            }

            if (playerItemNameText != null && playerItem != null)
            {
                playerItemNameText.text = $"Give: {playerItem.ItemName}";
                playerItemNameText.gameObject.SetActive(true);
            }

            if (npcItemImage != null && npcItem != null)
            {
                npcItemImage.sprite = npcItem.ItemIcon;
                npcItemImage.gameObject.SetActive(true);
            }

            if (npcItemNameText != null && npcItem != null)
            {
                npcItemNameText.text = $"Receive: {npcItem.ItemName}";
                npcItemNameText.gameObject.SetActive(true);
            }

            tradeConfirmationPanel.SetActive(true);
        }

        public void HideTradeConfirmationPanel()
        {
            if (tradeConfirmationPanel != null)
                tradeConfirmationPanel.SetActive(false);
        }

        private void OnConfirmTradeClicked() => uiService?.OnTradeConfirmed(currentNPCName);

        private void OnCancelTradeClicked() => uiService?.OnTradeCancelled();

        private void OnDestroy()
        {
            if (confirmTradeButton != null)
                confirmTradeButton.onClick.RemoveListener(OnConfirmTradeClicked);

            if (cancelTradeButton != null)
                cancelTradeButton.onClick.RemoveListener(OnCancelTradeClicked);
        }
    }
}