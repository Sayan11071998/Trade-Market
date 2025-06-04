using UnityEngine;

namespace TradeMarket.PlayerSystem
{
    [CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "Player/PlayerScriptableObject")]
    public class PlayerScriptableObject : ScriptableObject
    {
        [Header("Player Movement")]
        public float playerMovementSpeed = 5f;
    }
}