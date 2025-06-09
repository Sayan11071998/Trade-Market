using System;
using UnityEngine;
using TradeMarket.PlayerSystem;

namespace TradeMarket.SaveSystem
{
    public class PlayerSaveManager
    {
        private PlayerDataScriptableObject playerData;

        public PlayerSaveManager(PlayerDataScriptableObject data) => playerData = data;

        public void SavePlayerState(PlayerModel playerModel)
        {
            if (playerData == null) return;

            playerData.currentItem = playerModel.CurrentItem;
            playerData.isInventoryOpen = playerModel.IsInventoryOpen;
            playerData.lastMovement = playerModel.LastMovement;
            playerData.hasInitializedThisSession = true;
        }

        public void LoadPlayerState(PlayerModel playerModel)
        {
            if (playerData == null || !playerData.hasInitializedThisSession) return;

            playerModel.SetItem(playerData.currentItem);
            if (playerData.isInventoryOpen != playerModel.IsInventoryOpen)
                playerModel.SetInventoryOpen(playerData.isInventoryOpen);
        }

        public void CompleteScene(string sceneName)
        {
            if (playerData == null || IsSceneCompleted(sceneName)) return;

            var newArray = new string[playerData.completedSceneNames.Length + 1];
            Array.Copy(playerData.completedSceneNames, newArray, playerData.completedSceneNames.Length);
            newArray[playerData.completedSceneNames.Length] = sceneName;

            playerData.completedSceneNames = newArray;
            playerData.scenesCompleted++;
        }

        public bool IsSceneCompleted(string sceneName)
        {
            if (playerData == null) return false;
            return Array.IndexOf(playerData.completedSceneNames, sceneName) != -1;
        }

        public void ResetData()
        {
            if (playerData == null) return;

            playerData.currentItem = null;
            playerData.isInventoryOpen = false;
            playerData.lastMovement = Vector2.zero;
            playerData.scenesCompleted = 0;
            playerData.completedSceneNames = new string[0];
            playerData.hasInitializedThisSession = false;
        }
    }
}