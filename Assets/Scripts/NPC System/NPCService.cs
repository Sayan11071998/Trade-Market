using TradeMarket.ItemSystem;

namespace TradeMarket.NPCSystem
{
    public class NPCService
    {
        public NPCModel NPCModel { get; private set; }
        public NPCController NPCController { get; private set; }
        public NPCView NPCView { get; private set; }

        public NPCService(NPCView npcViewToSet, NPCScriptableObject npcScriptableObjectToSet)
        {
            NPCView = npcViewToSet;
            NPCModel = new NPCModel(npcScriptableObjectToSet);
            NPCController = new NPCController(NPCModel);

            NPCView.SetController(NPCController);
            NPCController.SetView(NPCView);
            NPCView.Initialize(npcScriptableObjectToSet);
        }

        public bool CanPlayerTrade(ItemScriptableObject playerItem) => NPCController.CanTrade(playerItem);

        public ItemScriptableObject ExecuteTrade(ItemScriptableObject playerItem) => NPCController.ExecuteTrade(playerItem);

        public string GetNPCName() => NPCModel.NPCName;

        public bool HasTraded() => NPCModel.HasTraded;
    }
}