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
                    HandleFinalScene();
                else
                    HandleSceneTransition();
            }
            else
            {
                ShowRequiredItemMessage();
            }
        }

        private void HandleFinalScene()
        {
            var playerController = GameService.Instance.playerService.PlayerController;
            playerController.SetCurrentItem(null);
            playerController.DisableControls();
            GameService.Instance.uiService.ShowGameCompletion();
            CompleteCurrentScene();
        }

        private void HandleSceneTransition()
        {
            GameService.Instance.SaveGameState();
            CompleteCurrentScene();
            StartSceneTransition();
        }

        private void ShowRequiredItemMessage()
        {
            GameService.Instance.playerService.PlayerController.PlayerView.ActivateDialoguePanel();
            GameService.Instance.playerService.PlayerController.PlayerView.ShowDialogue(GameString.DoNotHaveRequiredItem);
        }

        private void CompleteCurrentScene()
        {
            var playerData = GameService.Instance.playerService.PlayerModel.GetPersistentData();
            if (playerData != null)
            {
                var saveManager = new PlayerSaveManager(playerData);
                saveManager.CompleteScene(currentSceneName);
            }
        }

        private void StartSceneTransition()
        {
            fadeAnimator.Play(GameString.SceneAnimationFadeToBlack);
            StartCoroutine(LoadDelay());
        }

        private IEnumerator LoadDelay()
        {
            yield return new WaitForSeconds(fadeTimeDelay);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}