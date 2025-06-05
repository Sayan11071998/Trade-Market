using System.Collections.Generic;
using TradeMarket.ItemSystem;

namespace TradeMarket.NPCSystem
{
    [System.Serializable]
    public class NPCSetup
    {
        public NPCView npcView;
        public NPCScriptableObject npcData;
    }

    public class NPCManager
    {
        private NPCRepository npcRepository;

        public NPCManager(List<NPCSetup> npcSetups)
        {
            npcRepository = new NPCRepository();
            InitializeNPCs(npcSetups);
        }

        private void InitializeNPCs(List<NPCSetup> npcSetups)
        {
            foreach (var npcSetup in npcSetups)
            {
                if (npcSetup.npcView == null || npcSetup.npcData == null) continue;

                NPCController npcController = NPCFactory.CreateNPC(npcSetup.npcView, npcSetup.npcData);
                npcRepository.AddNPC(npcController);
            }
        }

        public NPCController GetNPCByName(string npcName) => npcRepository.GetNPCByName(npcName);
        public List<NPCController> GetAllNPCs() => npcRepository.GetAllNPCs();
        public List<NPCController> GetNPCsWantingItem(ItemScriptableObject item) => npcRepository.GetNPCsWantingItem(item);
        public int GetTradedNPCCount() => npcRepository.GetTradedNPCCount();
    }
}