using UnityEngine;
using TradeMarket.Core;
using TradeMarket.Utilities;

namespace TradeMarket.SceneTransitionSystem
{
    public class SceneTransitionHandler : MonoBehaviour
    {
        [SerializeField] private bool isFinalScene;
        [SerializeField] private ScriptableObject ItemRequiredToComplete;

        private bool IsPlayerHavingTheRequiredItem()
        {
            var playerItem = GameService.Instance.playerService.PlayerModel.CurrentItem;

            if (playerItem == ItemRequiredToComplete)
                return true;

            return false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(GameString.PlayerTag))
            {
                if (IsPlayerHavingTheRequiredItem() && isFinalScene)
                    Debug.Log("Game Won!!");
                else
                    Debug.Log("Scene Completed");
            }
        }
    }
}