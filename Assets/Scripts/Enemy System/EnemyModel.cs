using UnityEngine;

namespace TradeMarket.EnemySystem
{
    public class EnemyModel
    {
        public float EnemyHealth { get; private set; }

        public float FireCooldown { get; private set; }
        public bool CanFire { get; private set; }
        public float lastFireTime = -1f;

        public EnemyModel(EnemyScriptableObject enemyData)
        {
            EnemyHealth = enemyData.EnemyHealth;
            FireCooldown = enemyData.fireCooldown;
            CanFire = false;
        }

        public void EnableFire(bool value) => CanFire = value;

        public bool CanFireNow()
        {
            if (!CanFire) return false;

            return lastFireTime < 0f || Time.time >= lastFireTime + FireCooldown;
        }

        public void RegisterFire() => lastFireTime = Time.time;
    }
}