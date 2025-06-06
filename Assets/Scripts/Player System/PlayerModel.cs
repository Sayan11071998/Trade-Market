using System;
using UnityEngine;
using TradeMarket.ItemSystem;

namespace TradeMarket.PlayerSystem
{
    public class PlayerModel
    {
        public float MovementSpeed { get; private set; }
        public Vector2 Movement { get; private set; }
        public Vector2 LastMovement { get; private set; }
        public bool IsWalking { get; private set; }
        public ItemScriptableObject CurrentItem { get; private set; }
        public bool IsInventoryOpen { get; private set; }
        public bool IsTradeUIActive { get; private set; }

        public event Action OnInventoryToggled;
        public event Action OnItemChanged;

        private PlayerDataScriptableObject playerData;

        public PlayerModel(PlayerScriptableObject playerScriptableObject, PlayerDataScriptableObject playerDataSO = null)
        {
            MovementSpeed = playerScriptableObject.playerMovementSpeed;
            Movement = Vector2.zero;
            LastMovement = Vector2.zero;
            IsWalking = false;
            CurrentItem = null;
            IsInventoryOpen = false;
            IsTradeUIActive = false;

            playerData = playerDataSO;

            if (playerData != null)
                playerData.LoadPlayerState(this);
        }

        public void SetMovement(Vector2 newMovement)
        {
            if (IsTradeUIActive)
            {
                StopPlayerMovement();
                return;
            }

            Movement = newMovement;
            IsWalking = newMovement.magnitude > 0.1f;

            if (newMovement != Vector2.zero)
            {
                LastMovement = newMovement;

                if (playerData != null)
                    playerData.lastMovement = LastMovement;
            }
        }

        private void StopPlayerMovement()
        {
            Movement = Vector2.zero;
            IsWalking = false;
        }

        public void SetItem(ItemScriptableObject item)
        {
            CurrentItem = item;

            if (playerData != null)
                playerData.currentItem = item;

            OnItemChanged?.Invoke();
        }

        private Vector2 PlayerVelocity() => Movement * MovementSpeed;
        public Vector2 PlayerMovementVelocity => PlayerVelocity();

        public void ToggleInventory()
        {
            IsInventoryOpen = !IsInventoryOpen;

            if (playerData != null)
                playerData.isInventoryOpen = IsInventoryOpen;

            OnInventoryToggled?.Invoke();
        }

        public void SetTradeUIActive(bool isActive) => IsTradeUIActive = isActive;

        public void SaveState()
        {
            if (playerData != null)
                playerData.SavePlayerState(this);
        }

        public PlayerDataScriptableObject GetPersistentData() => playerData;
    }
}