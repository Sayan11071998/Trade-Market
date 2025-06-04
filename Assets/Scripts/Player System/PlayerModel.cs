using UnityEngine;

namespace TradeMarket.PlayerSystem
{
    public class PlayerModel
    {
        public float MovementSpeed { get; private set; }
        public Vector2 Movement { get; private set; }
        public Vector2 LastMovement { get; private set; }

        public PlayerModel(float movementSpeed)
        {
            MovementSpeed = movementSpeed;
            Movement = Vector2.zero;
            LastMovement = Vector2.zero;
        }

        public void SetMovement(Vector2 newMovement)
        {
            Movement = newMovement;
            if (newMovement != Vector2.zero)
            {
                LastMovement = newMovement;
            }
        }
    }
}