using UnityEngine;
using TradeMarket.ItemSystem;
using TradeMarket.Core;

namespace TradeMarket.NPCSystem
{
    public class NPCController
    {
        private NPCModel npcModel;
        private NPCView npcView;

        private bool hasGreeted = false;
        private bool hasShownTradeOffer = false; // Add this new state

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

            if (!hasGreeted)
            {
                hasGreeted = true;
                npcView?.ShowDialogue(npcModel.GreetingText);
                return;
            }

            // Show trade offer first
            if (!hasShownTradeOffer)
            {
                hasShownTradeOffer = true;
                npcView?.ShowDialogue(npcModel.TradeOfferText);
                return;
            }

            // Only after trade offer has been shown, check for trade confirmation
            var playerItem = GameService.Instance.playerService.PlayerModel.CurrentItem;
            if (CanTrade(playerItem))
            {
                // Show trade confirmation UI
                GameService.Instance.uiService.ShowTradeConfirmation(npcModel.NPCName, playerItem, npcModel.ItemNPCHaving);
            }
            else
            {
                // Show can't do trade text if player doesn't have the right item
                npcView?.ShowDialogue(npcModel.CantDoTradeText);
            }
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

        public void ResetTradeOfferState()
        {
            hasShownTradeOffer = false;
        }

        public string GetCurrentDialogue()
        {
            if (npcModel.HasTraded)
                return npcModel.AlreadyTradedText;
            else if (!hasGreeted)
                return npcModel.GreetingText;
            else
                return npcModel.TradeOfferText;
        }
    }
}