using System;
using UnityEngine;
using TradeMarket.ItemSystem;

namespace TradeMarket.PlayerSystem
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
        public string[] completedSceneNames;

        [Header("Runtime Data")]
        public bool hasInitializedThisSession;

        public void SavePlayerState(PlayerModel playerModel)
        {
            currentItem = playerModel.CurrentItem;
            isInventoryOpen = playerModel.IsInventoryOpen;
            lastMovement = playerModel.LastMovement;
            hasInitializedThisSession = true;
        }

        public void LoadPlayerState(PlayerModel playerModel)
        {
            if (hasInitializedThisSession)
            {
                playerModel.SetItem(currentItem);
                if (isInventoryOpen != playerModel.IsInventoryOpen)
                    playerModel.SetInventoryOpen(isInventoryOpen);
            }
        }

        public void ResetData()
        {
            currentItem = null;
            isInventoryOpen = false;
            lastMovement = Vector2.zero;
            scenesCompleted = 0;
            completedSceneNames = new string[0];
            hasInitializedThisSession = false;
        }

        public void CompleteScene(string sceneName)
        {
            if (Array.IndexOf(completedSceneNames, sceneName) == -1)
            {
                var newArray = new string[completedSceneNames.Length + 1];
                Array.Copy(completedSceneNames, newArray, completedSceneNames.Length);
                newArray[completedSceneNames.Length] = sceneName;
                completedSceneNames = newArray;
                scenesCompleted++;
            }
        }

        public bool IsSceneCompleted(string sceneName) => Array.IndexOf(completedSceneNames, sceneName) != -1;
    }
}