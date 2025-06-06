using UnityEngine;
using TradeMarket.ItemSystem;

namespace TradeMarket.SaveSystem
{
    [CreateAssetMenu(fileName = "PlayerSaveDataScriptableObject", menuName = "SaveData/PlayerDataScriptableObject")]
    public class PlayerDataScriptableObject : ScriptableObject
    {
        [Header("Player State")]
        public ItemScriptableObject currentItem;
        public bool isInventoryOpen;
        public Vector2 lastMovement = Vector2.zero;

        [Header("Game Progress")]
        public int scenesCompleted;
        public string[] completedSceneNames = new string[0];

        [Header("Runtime Data")]
        public bool hasInitializedThisSession;
    }
}