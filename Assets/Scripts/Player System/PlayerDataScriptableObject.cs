using UnityEngine;
using TradeMarket.ItemSystem;

namespace TradeMarket.PlayerSystem
{
    [CreateAssetMenu(fileName = "PlayerDataScriptableObject", menuName = "Player/PlayerDataScriptableObject")]
    public class PlayerDataScriptableObject : ScriptableObject
    {
        [Header("Player State")]
        public ItemScriptableObject currentItem;
        public bool isInventoryOpen;
        public Vector2 lastMovement = Vector2.zero;
        
        [Header("Game Progress")]
        public int scenesCompleted;
        public string[] completedSceneNames;
        
        [Header("Runtime Data")]
        public bool hasInitializedThisSession;

        // Method to save current player state
        public void SavePlayerState(PlayerModel playerModel)
        {
            currentItem = playerModel.CurrentItem;
            isInventoryOpen = playerModel.IsInventoryOpen;
            lastMovement = playerModel.LastMovement;
            hasInitializedThisSession = true;
        }

        // Method to load state into player model
        public void LoadPlayerState(PlayerModel playerModel)
        {
            if (hasInitializedThisSession)
            {
                playerModel.SetItem(currentItem);
                if (isInventoryOpen != playerModel.IsInventoryOpen)
                    playerModel.ToggleInventory();
            }
        }

        // Method to reset data (useful for new game)
        public void ResetData()
        {
            currentItem = null;
            isInventoryOpen = false;
            lastMovement = Vector2.zero;
            scenesCompleted = 0;
            completedSceneNames = new string[0];
            hasInitializedThisSession = false;
        }

        // Method to mark scene as completed
        public void CompleteScene(string sceneName)
        {
            if (System.Array.IndexOf(completedSceneNames, sceneName) == -1)
            {
                var newArray = new string[completedSceneNames.Length + 1];
                System.Array.Copy(completedSceneNames, newArray, completedSceneNames.Length);
                newArray[completedSceneNames.Length] = sceneName;
                completedSceneNames = newArray;
                scenesCompleted++;
            }
        }

        // Check if scene is completed
        public bool IsSceneCompleted(string sceneName)
        {
            return System.Array.IndexOf(completedSceneNames, sceneName) != -1;
        }
    }
}