using UnityEngine;
using TradeMarket.BulletSystem;

namespace TradeMarket.EnemySystem
{
    public class EnemyView : MonoBehaviour
    {
        [Header("Visual Components")]
        [SerializeField] private SpriteRenderer enemySpriteRenderer;

        [Header("Shooting")]
        [SerializeField] private BulletView enemyBulletPrefab;
        [SerializeField] private BulletScriptableObject enemyBulletData;
        [SerializeField] private Transform firePoint;
        [SerializeField] private Transform bulletPoolParent;

        private EnemyController enemyController;
        private BulletService enemyBulletService;

        public EnemyController EnemyController => enemyController;

        public void SetController(EnemyController enemyControllerToSet) => enemyController = enemyControllerToSet;

        public void Initialize(EnemyScriptableObject enemyData)
        {
            if (enemySpriteRenderer != null && enemyData.enemySprite != null)
                enemySpriteRenderer.sprite = enemyData.enemySprite;

            enemyBulletService = new BulletService(enemyBulletPrefab, enemyBulletData, bulletPoolParent);
        }

        private void Update()
        {
            UpdateEnemy();
            HandleDeath();
        }

        public void UpdateEnemy()
        {
            enemyBulletService?.UpdateBullets();

            if (enemyController?.EnemyModel != null && enemyController.EnemyModel.CanFire)
            {
                if (enemyController.TryFire(out Vector2 fireDirection))
                    FireAtPlayer(fireDirection);
            }
        }

        private void FireAtPlayer(Vector2 fireDirection)
        {
            if (enemyBulletService != null && firePoint != null)
                enemyBulletService.FireBullet(firePoint.position, fireDirection);
        }

        private void HandleDeath()
        {
            if (enemyController.EnemyModel.IsDead)
            {
                enemyController.DisableFire();
                Destroy(gameObject);
            }
        }

        private void OnDestroy() => enemyBulletService?.DeactivateAllBullets();
    }
}