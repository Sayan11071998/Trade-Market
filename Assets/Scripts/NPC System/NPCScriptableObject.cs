using UnityEngine;
using TradeMarket.ItemSystem;

namespace TradeMarket.NPCSystem
{
    [CreateAssetMenu(fileName = "NPCScriptableObject", menuName = "NPC/NPCScriptableObject")]
    public class NPCScriptableObject : ScriptableObject
    {
        [Header("NPC Details")]
        public string npcName;

        [Header("Item Details")]
        public ItemScriptableObject itemHaving;
        public ItemScriptableObject itemDesired;

        [Header("Visual")]
        public Sprite npcSprite;

        [Header("Dialogue")]
        [TextArea(3, 5)]
        public string greetingText = "Hello there!";
        [TextArea(3, 5)]
        public string tradeOfferText = "I have {itemHaving} and I want {itemDesired}.";
        [TextArea(3, 5)]
        public string alreadyTradedText = "Thanks for the trade!";
    }
}