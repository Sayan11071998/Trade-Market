using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TradeMarket.ItemSystem;
using TradeMarket.Utilities;
using TradeMarket.Core;
using TradeMarket.SoundSystem;

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

        [Header("Game UI Panel")]
        [SerializeField] private GameObject gameUIPanel;
        [SerializeField] private TextMeshProUGUI gameUIPanelHeaderText;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button quitButton;

        private UIController uiController;
        private string currentNPCName;

        public void SetController(UIController controller) => uiController = controller;
        private void Start()
        {
            InitializeButtons();
            HideTradeConfirmationPanel();
        }

        private void InitializeButtons()
        {
            confirmTradeButton?.onClick.AddListener(OnConfirmTradeClicked);
            cancelTradeButton?.onClick.AddListener(OnCancelTradeClicked);
            restartButton?.onClick.AddListener(OnRestartClicked);
            quitButton?.onClick.AddListener(OnQuitClicked);
        }

        private void OnDestroy() => UnsubscribeButtons();

        private void UnsubscribeButtons()
        {
            confirmTradeButton?.onClick.RemoveListener(OnConfirmTradeClicked);
            cancelTradeButton?.onClick.RemoveListener(OnCancelTradeClicked);
            restartButton?.onClick.RemoveListener(OnRestartClicked);
            quitButton?.onClick.RemoveListener(OnQuitClicked);
        }

        public void SetInventoryPanelActive(bool isActive) => inventoryPanel.SetActive(isActive);

        public void UpdateInventoryItem(ItemScriptableObject item)
        {
            if (item != null)
                ShowInventoryItem(item);
            else
                HideInventoryItem();
        }

        private void ShowInventoryItem(ItemScriptableObject item)
        {
            itemSprite.sprite = item.ItemIcon;
            itemNameText.text = item.ItemName;
            SetInventoryItemVisibility(true);
        }

        private void HideInventoryItem() => SetInventoryItemVisibility(false);

        private void SetInventoryItemVisibility(bool isVisible)
        {
            itemSprite.gameObject.SetActive(isVisible);
            itemNameText.gameObject.SetActive(isVisible);
        }

        public void ShowTradeConfirmationPanel(string npcName, ItemScriptableObject playerItem, ItemScriptableObject npcItem)
        {
            if (tradeConfirmationPanel == null) return;

            currentNPCName = npcName;
            SetupTradeConfirmationContent(npcName, playerItem, npcItem);
            tradeConfirmationPanel.SetActive(true);
        }

        private void SetupTradeConfirmationContent(string npcName, ItemScriptableObject playerItem, ItemScriptableObject npcItem)
        {
            SetTradeDescription(npcName);
            SetupPlayerItemDisplay(playerItem);
            SetupNPCItemDisplay(npcItem);
        }

        private void SetTradeDescription(string npcName)
        {
            if (tradeDescriptionText != null)
                tradeDescriptionText.text = string.Format(GameString.TradePrompt, npcName);
        }

        private void SetupPlayerItemDisplay(ItemScriptableObject playerItem)
        {
            if (playerItem == null) return;

            if (playerItemImage != null)
            {
                playerItemImage.sprite = playerItem.ItemIcon;
                playerItemImage.gameObject.SetActive(true);
            }

            if (playerItemNameText != null)
            {
                playerItemNameText.text = string.Format(GameString.GiveItemText, playerItem.ItemName);
                playerItemNameText.gameObject.SetActive(true);
            }
        }

        private void SetupNPCItemDisplay(ItemScriptableObject npcItem)
        {
            if (npcItem == null) return;

            if (npcItemImage != null)
            {
                npcItemImage.sprite = npcItem.ItemIcon;
                npcItemImage.gameObject.SetActive(true);
            }

            if (npcItemNameText != null)
            {
                npcItemNameText.text = string.Format(GameString.ReceiveItemText, npcItem.ItemName);
                npcItemNameText.gameObject.SetActive(true);
            }
        }

        public void HideTradeConfirmationPanel() => tradeConfirmationPanel?.SetActive(false);

        public void ShowGameUIPanel(string message)
        {
            gameUIPanel.SetActive(true);
            gameUIPanelHeaderText.text = message;
        }

        private void OnConfirmTradeClicked()
        {
            PlayButtonClickSound();
            uiController?.OnTradeConfirmed(currentNPCName);
        }

        private void OnCancelTradeClicked()
        {
            PlayButtonClickSound();
            uiController?.OnTradeCancelled();
        }

        private void OnRestartClicked()
        {
            PlayButtonClickSound();
            uiController?.RestartGame();
        }

        private void OnQuitClicked()
        {
            PlayButtonClickSound();
            uiController?.QuitGame();
        }

        private void PlayButtonClickSound() => SoundManager.Instance.soundService.PlaySoundEffects(SoundType.ButtonClick);
    }
}