namespace TradeMarket.NPCSystem
{
    public class NPCFactory
    {
        public static NPCController CreateNPC(NPCView npcView, NPCScriptableObject npcData)
        {
            var npcModel = new NPCModel(npcData);
            var npcController = new NPCController(npcModel, npcView);

            npcView.SetController(npcController);
            npcView.Initialize(npcData);

            return npcController;
        }
    }
}