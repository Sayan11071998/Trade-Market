using UnityEngine;

namespace TradeMarket.EnemySystem
{
    public class EnemyView : MonoBehaviour
    {
        [Header("Visual Components")]
        [SerializeField] private SpriteRenderer enemySpriteRenderer;

        private EnemyController enemyController;

        public EnemyController EnemyController => enemyController;

        public void SetController(EnemyController enemyControllerToSet) => enemyController = enemyControllerToSet;

        public void Initialize(EnemyScriptableObject enemyData)
        {
            if (enemySpriteRenderer != null && enemyData.enemySprite != null)
                enemySpriteRenderer.sprite = enemyData.enemySprite;
        }

        // Add simple behavior methods here if needed
        // public void UpdateEnemy()
        // {
        //     if (enemyController?.EnemyModel != null)
        //     {
        //         enemyController.EnemyModel.UpdateFireCooldown(Time.time);

        //         // Add your simple enemy behaviors here
        //         // For example: movement, simple AI, etc.
        //     }
        // }
    }
}