namespace TradeMarket.NPCSystem
{
    public class NPCService
    {
        public NPCModel npcModel { get; private set; }
        public NPCController npcController { get; private set; }
        public NPCView npcView { get; private set; }

        public NPCService(NPCView npcViewToSet, NPCScriptableObject npcScriptableObjectToSet)
        {
            npcView = npcViewToSet;
            npcModel = new NPCModel(npcScriptableObjectToSet);
            npcController = new NPCController(npcModel);
            npcView.SetController(npcController);
        }
    }
}