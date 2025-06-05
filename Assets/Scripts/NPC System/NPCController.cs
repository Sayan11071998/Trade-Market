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
                npcView?.ShowDialogue(GetFormattedText(npcModel.AlreadyTradedText));
                return;
            }

            if (!hasGreeted)
            {
                hasGreeted = true;
                npcView?.ShowDialogue(GetFormattedText(npcModel.GreetingText));
                return;
            }

            if (!hasShownTradeOffer)
            {
                hasShownTradeOffer = true;
                npcView?.ShowDialogue(GetFormattedText(npcModel.TradeOfferText));
                return;
            }

            var playerItem = GameService.Instance.playerService.PlayerModel.CurrentItem;
            if (CanTrade(playerItem))
                GameService.Instance.uiService.ShowTradeConfirmation(npcModel.NPCName, playerItem, npcModel.ItemNPCHaving);
            else
                npcView?.ShowDialogue(GetFormattedText(npcModel.CantDoTradeText));
        }

        public bool CanTrade(ItemScriptableObject playerItem) => !npcModel.HasTraded && playerItem != null && playerItem == npcModel.ItemDesired;

        public ItemScriptableObject ExecuteTrade(ItemScriptableObject playerItem)
        {
            if (!CanTrade(playerItem)) return null;

            ItemScriptableObject npcItem = npcModel.ItemNPCHaving;
            npcModel.CompleteTrade();

            npcView?.UpdateTradeStatus(true);
            npcView?.ShowDialogue(GetFormattedText(npcModel.AlreadyTradedText));

            return npcItem;
        }

        public void ResetTradeOfferState() => hasShownTradeOffer = false;

        private string GetFormattedText(string text)
        {
            return text.Replace("{itemHaving}", npcModel.ItemNPCHaving?.ItemName ?? "nothing")
                      .Replace("{itemDesired}", npcModel.ItemDesired?.ItemName ?? "something");
        }
    }
}