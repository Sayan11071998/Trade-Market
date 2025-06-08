using UnityEngine;

namespace TradeMarket.EnemySystem
{
    public class EnemyScriptableObject : ScriptableObject
    {
        [Header("Enemy Stats")]
        public float EnemyHealth = 100f;

        [Header("Enemy Combat")]
        public float fireCooldown = 0.5f;
    }
}