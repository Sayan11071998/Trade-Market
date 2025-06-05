using UnityEngine;

namespace TradeMarket.NPCSystem
{
    public class NPCView : MonoBehaviour
    {
        private NPCController npcController;

        public void SetController(NPCController npcControllerToSet) => npcController = npcControllerToSet;
    }
}