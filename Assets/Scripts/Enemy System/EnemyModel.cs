namespace TradeMarket.EnemySystem
{
    public class EnemyModel
    {
        public float EnemyHealth { get; private set; }
        public float MaxHealth { get; private set; }

        public float FireCooldown { get; private set; }
        public bool CanFire { get; private set; }
        public float LastFireTime { get; private set; }

        public EnemyModel(EnemyScriptableObject enemyData)
        {
            EnemyHealth = enemyData.enemyHealth;
            MaxHealth = enemyData.enemyHealth;
            FireCooldown = enemyData.fireCooldown;
            CanFire = true;
            LastFireTime = -1f;
        }

        // public void TakeDamage(float damage)
        // {
        //     EnemyHealth = UnityEngine.Mathf.Max(0, EnemyHealth - damage);
        // }

        // public bool IsDead() => EnemyHealth <= 0;

        // public void UpdateFireCooldown(float currentTime)
        // {
        //     if (!CanFire && currentTime - LastFireTime >= FireCooldown)
        //     {
        //         CanFire = true;
        //     }
        // }

        // public void SetFired(float currentTime)
        // {
        //     CanFire = false;
        //     LastFireTime = currentTime;
        // }
    }
}