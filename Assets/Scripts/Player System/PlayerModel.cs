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

        public event Action OnInventoryToggled;

        public PlayerModel(PlayerScriptableObject playerScriptableObject)
        {
            MovementSpeed = playerScriptableObject.playerMovementSpeed;
            Movement = Vector2.zero;
            LastMovement = Vector2.zero;
            IsWalking = false;
            CurrentItem = null;
            IsInventoryOpen = false;
        }

        public void SetMovement(Vector2 newMovement)
        {
            Movement = newMovement;
            IsWalking = newMovement.magnitude > 0.1f;

            if (newMovement != Vector2.zero)
                LastMovement = newMovement;
        }

        public void SetItem(ItemScriptableObject item) => CurrentItem = item;

        public void RemoveItem() => CurrentItem = null;

        private Vector2 PlayerVelocity() => Movement * MovementSpeed;
        public Vector2 PlayerMovementVelocity => PlayerVelocity();

        public void ToggleInventory()
        {
            IsInventoryOpen = !IsInventoryOpen;
            OnInventoryToggled?.Invoke();
        }
    }
}