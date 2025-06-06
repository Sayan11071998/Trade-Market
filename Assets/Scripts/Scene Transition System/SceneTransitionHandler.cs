using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TradeMarket.Core;
using TradeMarket.Utilities;
using TradeMarket.SaveSystem;

namespace TradeMarket.SceneTransitionSystem
{
    public class SceneTransitionHandler : MonoBehaviour
    {
        [SerializeField] private bool isFinalScene;
        [SerializeField] private ScriptableObject itemRequiredToComplete;
        [SerializeField] private string currentSceneName;
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
            if (!other.CompareTag(GameString.PlayerTag)) return;

            bool hasRequiredItem = IsPlayerHavingTheRequiredItem();

            if (hasRequiredItem)
            {
                if (isFinalScene)
                {
                    Debug.Log("Game Won!!");
                    var playerData = GameService.Instance.playerService.PlayerModel.GetPersistentData();
                    if (playerData != null)
                    {
                        var saveManager = new PlayerSaveManager(playerData);
                        saveManager.CompleteScene(currentSceneName);
                    }
                }
                else
                {
                    GameService.Instance.SaveGameState();

                    var playerData = GameService.Instance.playerService.PlayerModel.GetPersistentData();
                    if (playerData != null)
                    {
                        var saveManager = new PlayerSaveManager(playerData);
                        saveManager.CompleteScene(currentSceneName);
                    }

                    fadeAnimator.Play(GameString.SceneAnimationFadeToBlack);
                    StartCoroutine(LoadDelay());
                }
            }
            else
            {
                GameService.Instance.playerService.PlayerController.PlayerView.ActivateDialoguePanel();
                GameService.Instance.playerService.PlayerController.PlayerView.ShowDialogue(GameString.DoNotHaveRequiredItem);
            }
        }

        private IEnumerator LoadDelay()
        {
            yield return new WaitForSeconds(fadeTimeDelay);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}