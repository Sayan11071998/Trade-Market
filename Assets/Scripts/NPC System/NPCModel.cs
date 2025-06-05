namespace TradeMarket.NPCSystem
{
    public class NPCModel
    {
        public string NPCName { get; private set; }
        public string GreetingText { get; private set; }

        public NPCModel(NPCScriptableObject npcData)
        {
            NPCName = npcData.npcName;
            GreetingText = npcData.greetingText;
        }
    }
}