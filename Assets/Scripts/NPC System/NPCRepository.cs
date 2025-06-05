using System.Collections.Generic;
using TradeMarket.ItemSystem;

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

        public List<NPCController> GetAllNPCs() => new List<NPCController>(allNPCs);

        public List<NPCController> GetNPCsWantingItem(ItemScriptableObject item)
        {
            List<NPCController> interestedNPCs = new List<NPCController>();

            foreach (var npc in allNPCs)
            {
                if (npc.CanTrade(item))
                    interestedNPCs.Add(npc);
            }

            return interestedNPCs;
        }

        public int GetTradedNPCCount()
        {
            int count = 0;
            foreach (var npc in allNPCs)
                if (npc.NPCModel.HasTraded) count++;
            return count;
        }
    }
}