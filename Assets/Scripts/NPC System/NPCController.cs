using UnityEngine;

namespace TradeMarket.NPCSystem
{
    public class NPCController
    {
        private NPCModel npcModel;
        private NPCView npcView;

        public NPCController(NPCModel npcModelToSet)
        {
            npcModel = npcModelToSet;
        }
    }
}