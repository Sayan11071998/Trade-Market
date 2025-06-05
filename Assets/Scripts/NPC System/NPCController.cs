using TradeMarket.ItemSystem;
using TradeMarket.Core;

namespace TradeMarket.NPCSystem
{
    public class NPCController
    {
        private NPCModel npcModel;
        private NPCView npcView;

        private bool hasGreeted = false;
        private bool hasShownTradeOffer = false;

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

            if (!hasShownTradeOffer)
            {
                hasShownTradeOffer = true;
                npcView?.ShowDialogue(npcModel.TradeOfferText);
                return;
            }

            var playerItem = GameService.Instance.playerService.PlayerModel.CurrentItem;
            if (CanTrade(playerItem))
                GameService.Instance.uiService.ShowTradeConfirmation(npcModel.NPCName, playerItem, npcModel.ItemNPCHaving);
            else
                npcView?.ShowDialogue(npcModel.CantDoTradeText);
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

            return npcItem;
        }

        public void ResetTradeOfferState() => hasShownTradeOffer = false;

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