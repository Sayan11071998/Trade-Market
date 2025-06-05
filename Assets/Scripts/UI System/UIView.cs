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

        private UIService uiService;

        public void SetService(UIService uiServiceToSet) => uiService = uiServiceToSet;

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
    }
}