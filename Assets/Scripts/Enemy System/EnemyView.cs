using UnityEngine;

namespace TradeMarket.EnemySystem
{
    public class EnemyView : MonoBehaviour
    {
        [Header("Visual Components")]
        [SerializeField] private SpriteRenderer enemySpriteRenderer;

        [Header("Combat")]
        [SerializeField] private Transform firePoint;

        private EnemyController enemyController;

        public EnemyController EnemyController => enemyController;

        public void SetController(EnemyController enemyControllerToSet) => enemyController = enemyControllerToSet;

        public void Initialize(EnemyScriptableObject enemyData)
        {
            if (enemySpriteRenderer != null && enemyData.enemySprite != null)
                enemySpriteRenderer.sprite = enemyData.enemySprite;
        }

        private void Update() => UpdateEnemy();

        public void UpdateEnemy()
        {
            if (enemyController?.EnemyModel != null && enemyController.EnemyModel.CanFire)
            {
                if (enemyController.TryFire(out Vector2 fireDirection))
                    FireAtPlayer(fireDirection);
            }
        }

        private void FireAtPlayer(Vector2 fireDirection)
        {
            Debug.Log($"Enemy {gameObject.name} is firing in direction: {fireDirection}");
        }
    }
}