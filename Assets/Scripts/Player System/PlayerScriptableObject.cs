using UnityEngine;

namespace TradeMarket.PlayerSystem
{
    [CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "Player/PlayerScriptableObject")]
    public class PlayerScriptableObject : ScriptableObject
    {
        [Header("Player Health")]
        public float PlayerHealth;

        [Header("Player Movement")]
        public float playerMovementSpeed = 5f;

        [Header("Player Combat")]
        public float fireCooldown = 0.5f;
    }
}