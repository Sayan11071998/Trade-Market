using System.Collections.Generic;

namespace TradeMarket.NPCSystem
{
    public class NPCRepository
    {
        private Dictionary<string, NPCController> npcsByName;
        private List<NPCController> allNPCs;

        public NPCRepository()
        {
            npcsByName = new Dictionary<string, NPCController>();
            allNPCs = new List<NPCController>();
        }

        public void AddNPC(NPCController npc)
        {
            allNPCs.Add(npc);
            npcsByName[npc.NPCModel.NPCName] = npc;
        }

        public NPCController GetNPCByName(string npcName)
        {
            npcsByName.TryGetValue(npcName, out NPCController npc);
            return npc;
        }
    }
}