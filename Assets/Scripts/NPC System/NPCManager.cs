using System.Collections.Generic;

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
        private List<NPCService> npcServices;
        private Dictionary<string, NPCService> npcServicesByName;

        public NPCManager(List<NPCSetup> npcSetups)
        {
            npcServices = new List<NPCService>();
            npcServicesByName = new Dictionary<string, NPCService>();

            InitializeNPCs(npcSetups);
        }

        private void InitializeNPCs(List<NPCSetup> npcSetups)
        {
            foreach (var npcSetup in npcSetups)
            {
                if (npcSetup.npcView == null || npcSetup.npcData == null) continue;

                NPCService npcService = new NPCService(npcSetup.npcView, npcSetup.npcData);
                npcServices.Add(npcService);
                npcServicesByName[npcService.GetNPCName()] = npcService;
            }
        }

        public NPCService GetNPCByName(string npcName)
        {
            npcServicesByName.TryGetValue(npcName, out NPCService npcService);
            return npcService;
        }

        public List<NPCService> GetAllNPCs() => new List<NPCService>(npcServices);

        public int GetNPCCount() => npcServices.Count;
    }
}