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
            return playerItem == ItemRequiredToComplete;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(GameString.PlayerTag))
                return;

            bool hasRequiredItem = IsPlayerHavingTheRequiredItem();

            if (hasRequiredItem)
            {
                if (isFinalScene)
                    Debug.Log("Game Won!!");
                else
                    Debug.Log("Scene Completed");
            }
            else
            {
                Debug.Log("Required item missing to complete this scene.");
            }
        }
    }
}