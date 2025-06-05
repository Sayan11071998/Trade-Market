using UnityEngine;

namespace TradeMarket.NPCSystem
{
    public class NPCController
    {
        private NPCModel npcModel;
        private NPCView npcView;

        public NPCModel NPCModel => npcModel;

        public NPCController(NPCModel npcModelToSet) => npcModel = npcModelToSet;

        public void SetView(NPCView npcViewToSet) => npcView = npcViewToSet;

        public void OnPlayerInteract()
        {
            Debug.Log($"{npcModel.NPCName}: {npcModel.GreetingText}");
            npcView?.ShowDialogue(npcModel.GreetingText);
        }

        public string GetCurrentDialogue() => npcModel.GreetingText;
    }
}