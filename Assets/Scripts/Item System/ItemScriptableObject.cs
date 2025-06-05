using UnityEngine;

namespace TradeMarket.ItemSystem
{
    [CreateAssetMenu(fileName = "ItemScriptableObject", menuName = "Item/ItemScriptableObject")]
    public class ItemScriptableObject : ScriptableObject
    {
        public string ItemName;
        public Sprite ItemIcon;
    }
}