using UnityEngine;
using TradeMarket.ItemSystem;

namespace TradeMarket.NPCSystem
{
    public class NPCController
    {
        private NPCModel npcModel;
        private NPCView npcView;

        public NPCModel NPCModel => npcModel;

        public NPCController(NPCModel npcModelToSet) => npcModel = npcModelToSet;

        public void SetView(NPCView npcViewToSet) => npcView = npcViewToSet;

        public void OnPlayerInteract()
        {
            if (npcModel.HasTraded)
            {
                npcView?.ShowDialogue(npcModel.AlreadyTradedText);
                return;
            }

            npcView?.ShowDialogue(npcModel.TradeOfferText);
        }

        public bool CanTrade(ItemScriptableObject playerItem)
        {
            if (npcModel.HasTraded) return false;
            if (playerItem == null) return false;

            return playerItem == npcModel.ItemDesired;
        }

        public ItemScriptableObject ExecuteTrade(ItemScriptableObject playerItem)
        {
            if (!CanTrade(playerItem)) return null;

            ItemScriptableObject npcItem = npcModel.ItemNPCHaving;
            npcModel.CompleteTrade();

            npcView?.UpdateTradeStatus(true);
            npcView?.ShowDialogue(npcModel.AlreadyTradedText);

            Debug.Log($"Trade completed! Player gave {playerItem.ItemName}, received {npcItem.ItemName}");

            return npcItem;
        }

        public string GetCurrentDialogue()
        {
            if (npcModel.HasTraded)
                return npcModel.AlreadyTradedText;
            else
                return npcModel.TradeOfferText;
        }
    }
}