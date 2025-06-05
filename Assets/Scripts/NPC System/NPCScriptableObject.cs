using UnityEngine;
using TradeMarket.ItemSystem;

namespace TradeMarket.NPCSystem
{
    [CreateAssetMenu(fileName = "NPCScriptableObject", menuName = "NPC/NPCScriptableObject")]
    public class NPCScriptableObject : ScriptableObject
    {
        [Header("NPC Details")]
        public string npcName;

        [Header("NPC Item Details")]
        public ItemScriptableObject itemHaving;
        public ItemScriptableObject itemDesired;

        [Header("NPC Visual")]
        public Sprite npcSprite;

        [Header("NPC Dialogue")]
        [TextArea(3, 5)]
        public string greetingText = "Hello there!";
        [TextArea(3, 5)]
        public string tradeOfferText = "I have {itemHaving} and I want {itemDesired}.";
        [TextArea(3, 5)]
        public string cantDoTradeText = "You don't have what I need. Come back when you have {itemDesired}.";
        [TextArea(3, 5)]
        public string alreadyTradedText = "Thanks for the trade!";
    }
}