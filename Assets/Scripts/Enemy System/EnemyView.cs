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

            InitializeBulletService();
        }

        private void InitializeBulletService()
        {
            if (enemyBulletPrefab != null && enemyBulletData != null)
            {
                enemyBulletService = new BulletService(enemyBulletPrefab, enemyBulletData, bulletPoolParent);
            }
            else
            {
                Debug.LogError($"Enemy {gameObject.name}: Missing bullet prefab or bullet data!");
            }
        }

        private void Update() => UpdateEnemy();

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
            {
                enemyBulletService.FireBullet(firePoint.position, fireDirection);
                Debug.Log($"Enemy {gameObject.name} is firing in direction: {fireDirection}");
            }
            else
            {
                Debug.LogWarning($"Enemy {gameObject.name}: Cannot fire - missing bullet service or fire point!");
            }
        }

        private void OnDestroy()
        {
            // Clean up bullets when enemy is destroyed
            enemyBulletService?.DeactivateAllBullets();
        }
    }
}