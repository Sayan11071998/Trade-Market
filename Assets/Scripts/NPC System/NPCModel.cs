using UnityEngine;

namespace TradeMarket.NPCSystem
{
    public class NPCModel
    {
        public GameObject ItemNPCHaving { get; private set; }
        public GameObject ItemDesired { get; private set; }

        public bool HasTraded { get; set; }

        public NPCModel(NPCScriptableObject npcData)
        {
            ItemNPCHaving = npcData.itemHaving;
            ItemDesired = npcData.itemDesired;
            HasTraded = false;
        }
    }
}