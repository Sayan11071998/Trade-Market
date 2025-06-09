using TradeMarket.ItemSystem;

namespace TradeMarket.NPCSystem
{
    public class NPCModel
    {
        public string NPCName { get; private set; }

        public ItemScriptableObject ItemNPCHaving { get; private set; }
        public ItemScriptableObject ItemDesired { get; private set; }

        public bool HasTraded { get; private set; }

        public string GreetingText { get; private set; }
        public string TradeOfferText { get; private set; }
        public string CantDoTradeText { get; private set; }
        public string AlreadyTradedText { get; private set; }

        public NPCModel(NPCScriptableObject npcData)
        {
            NPCName = npcData.npcName;
            ItemNPCHaving = npcData.itemHaving;
            ItemDesired = npcData.itemDesired;
            HasTraded = false;

            GreetingText = npcData.greetingText;
            TradeOfferText = npcData.tradeOfferText;
            CantDoTradeText = npcData.cantDoTradeText;
            AlreadyTradedText = npcData.alreadyTradedText;
        }

        public void CompleteTrade() => HasTraded = true;
    }
}