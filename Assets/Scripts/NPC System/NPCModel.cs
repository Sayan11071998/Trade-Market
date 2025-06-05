using UnityEngine;

namespace TradeMarket.NPCSystem
{
    public class NPCModel
    {
        public GameObject itemNPCHaving { get; private set; }

        public bool hasTraded;

        public NPCModel(NPCScriptableObject npcData)
        {
            itemNPCHaving = npcData.itemHaving;
            hasTraded = false;
        }
    }
}