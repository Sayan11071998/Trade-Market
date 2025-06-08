using UnityEngine;

namespace TradeMarket.EnemySystem
{
    public class EnemyModel
    {
        public float EnemyHealth { get; private set; }
        public float MaxHealth { get; private set; }
        public bool IsDead { get; private set; }

        public float FireCooldown { get; private set; }
        public bool CanFire { get; private set; }
        public float LastFireTime { get; private set; }

        public EnemyModel(EnemyScriptableObject enemyData)
        {
            MaxHealth = enemyData.enemyHealth;
            EnemyHealth = MaxHealth;
            IsDead = false;
            FireCooldown = enemyData.fireCooldown;
            CanFire = false;
            LastFireTime = -1f;
        }

        public void EnableFire(bool enabled) => CanFire = enabled;

        public bool CanFireNow()
        {
            if (!CanFire) return false;

            return LastFireTime < 0f || Time.time >= LastFireTime + FireCooldown;
        }

        public void RegisterFire() => LastFireTime = Time.time;

        public void TakeDamage(float damage)
        {
            EnemyHealth = Mathf.Max(0, EnemyHealth - damage);

            if (EnemyHealth <= 0)
                IsDead = true;
        }
    }
}