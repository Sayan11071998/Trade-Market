using UnityEngine;

namespace TradeMarket.EnemySystem
{
    [CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "Enemy/EnemyScriptableObject")]
    public class EnemyScriptableObject : ScriptableObject
    {
        [Header("Enemy Stats")]
        public float enemyHealth = 100f;

        [Header("Enemy Combat")]
        public float fireCooldown = 0.5f;

        [Header("Enemy Visual")]
        public Sprite enemySprite;
    }
}