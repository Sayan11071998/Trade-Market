using UnityEngine;

namespace TradeMarket.NPCSystem
{
    [CreateAssetMenu(fileName = "NPCScriptableObject", menuName = "NPC/NPCScriptableObject")]
    public class NPCScriptableObject : ScriptableObject
    {
        [Header("Item Details")]
        public GameObject itemHaving;
        public GameObject itemDesired;
    }
}