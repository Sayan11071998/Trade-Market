using UnityEngine;
using TradeMarket.Core;
using TradeMarket.Utilities;
using UnityEngine.SceneManagement;
using System.Collections;

namespace TradeMarket.SceneTransitionSystem
{
    public class SceneTransitionHandler : MonoBehaviour
    {
        [SerializeField] private bool isFinalScene;
        [SerializeField] private ScriptableObject itemRequiredToComplete;
        [SerializeField] private string sceneToLoad;
        [SerializeField] private Animator fadeAnimator;
        [SerializeField] private float fadeTimeDelay = 0.5f;

        private bool IsPlayerHavingTheRequiredItem()
        {
            var playerItem = GameService.Instance.playerService.PlayerModel.CurrentItem;
            return playerItem == itemRequiredToComplete;
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
                {
                    fadeAnimator.Play("FadeToBlack");
                    StartCoroutine(LoadDelay());
                }
            }
            else
            {
                Debug.Log("Required item missing to complete this scene.");
            }
        }

        private IEnumerator LoadDelay()
        {
            yield return new WaitForSeconds(fadeTimeDelay);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}