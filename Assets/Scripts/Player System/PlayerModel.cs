using System;
using UnityEngine;
using TradeMarket.ItemSystem;
using TradeMarket.SaveSystem;

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
        public bool IsInTradeMode { get; private set; }
        public bool AreControlsEnabled { get; private set; } = true;

        public bool CanFire { get; private set; } = false;
        public float FireCooldown { get; private set; } = 0.5f;
        private float lastFireTime = -1f;

        public event Action OnInventoryToggled;
        public event Action OnItemChanged;
        public event Action OnMovementChanged;
        public event Action OnTradeModeChanged;

        private PlayerDataScriptableObject playerData;
        private PlayerSaveManager saveManager;

        public PlayerModel(PlayerScriptableObject playerScriptableObject, PlayerDataScriptableObject playerDataSO = null)
        {
            MovementSpeed = playerScriptableObject.playerMovementSpeed;
            FireCooldown = playerScriptableObject.fireCooldown;
            Movement = Vector2.zero;
            LastMovement = Vector2.zero;
            IsWalking = false;
            CurrentItem = null;
            IsInventoryOpen = false;
            IsInTradeMode = false;
            AreControlsEnabled = true;
            CanFire = false;
            lastFireTime = -1f;

            playerData = playerDataSO;

            if (playerData != null)
            {
                saveManager = new PlayerSaveManager(playerData);
                saveManager.LoadPlayerState(this);
            }
        }

        public void SetMovement(Vector2 newMovement)
        {
            if (!AreControlsEnabled) return;

            Movement = newMovement;
            OnMovementChanged?.Invoke();
        }

        public void SetIsWalking(bool walking) => IsWalking = walking;

        public void SetLastMovement(Vector2 lastMovement)
        {
            LastMovement = lastMovement;

            if (playerData != null)
                playerData.lastMovement = LastMovement;
        }

        public void SetItem(ItemScriptableObject item)
        {
            CurrentItem = item;

            if (playerData != null)
                playerData.currentItem = item;

            OnItemChanged?.Invoke();
        }

        public void SetInventoryOpen(bool isOpen)
        {
            if (!AreControlsEnabled) return;

            IsInventoryOpen = isOpen;

            if (playerData != null)
                playerData.isInventoryOpen = IsInventoryOpen;

            OnInventoryToggled?.Invoke();
        }

        public void SetTradeMode(bool isInTradeMode)
        {
            IsInTradeMode = isInTradeMode;
            OnTradeModeChanged?.Invoke();
        }

        public void SetControlsEnabled(bool enabled)
        {
            AreControlsEnabled = enabled;
            if (!enabled)
            {
                SetMovement(Vector2.zero);
                SetIsWalking(false);
            }
        }

        public void EnableFire(bool enabled) => CanFire = enabled;

        public bool CanFireNow()
        {
            if (!CanFire) return false;

            return lastFireTime < 0f || Time.time >= lastFireTime + FireCooldown;
        }

        public void RegisterFire() => lastFireTime = Time.time;

        public Vector2 GetMovementVelocity() => Movement * MovementSpeed;

        public void SaveState()
        {
            if (saveManager != null)
                saveManager.SavePlayerState(this);
        }

        public PlayerDataScriptableObject GetPersistentData() => playerData;
    }
}