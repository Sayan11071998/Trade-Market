using UnityEngine;

namespace TradeMarket.BulletSystem
{
    public class BulletModel
    {
        public float Speed { get; private set; }
        public float Damage { get; private set; }
        public float BulletLifeTime { get; private set; }
        public Vector2 Direction { get; private set; }
        public Vector2 StartPosition { get; private set; }
        public bool IsActive { get; private set; }
        public float CurrentLifeTime { get; private set; }

        public BulletModel(BulletScriptableObject bulletData)
        {
            Speed = bulletData.BulletSpeed;
            Damage = bulletData.BulletDamage;
            BulletLifeTime = bulletData.BulletLifetime;
            IsActive = false;
            CurrentLifeTime = 0f;
        }

        public void Initialize(Vector2 startPos, Vector2 direction)
        {
            StartPosition = startPos;
            Direction = direction.normalized;
            IsActive = true;
            CurrentLifeTime = 0f;
        }

        public void UpdateLifetime(float deltaTime)
        {
            if (IsActive)
            {
                CurrentLifeTime += deltaTime;
                if (CurrentLifeTime >= BulletLifeTime)
                {
                    IsActive = false;
                }
            }
        }

        public void Deactivate() => IsActive = false;
    }
}