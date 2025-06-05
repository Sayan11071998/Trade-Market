namespace TradeMarket.NPCSystem
{
    public class NPCFactory
    {
        public static NPCController CreateNPC(NPCView npcView, NPCScriptableObject npcData)
        {
            var npcModel = new NPCModel(npcData);
            var npcController = new NPCController(npcModel);

            npcView.SetController(npcController);
            npcController.SetView(npcView);
            npcView.Initialize(npcData);

            return npcController;
        }
    }
}