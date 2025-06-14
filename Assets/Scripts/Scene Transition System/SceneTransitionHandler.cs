using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TradeMarket.Core;
using TradeMarket.Utilities;
using TradeMarket.SaveSystem;
using TradeMarket.SoundSystem;

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
            bool hasKilledAllEnemies = GameService.Instance.enemyManager.GetAliveEnemiesCount() == 0;

            if (hasRequiredItem && hasKilledAllEnemies)
            {
                if (isFinalScene)
                    HandleFinalScene();
                else
                    HandleSceneTransition();
            }
            else if (hasRequiredItem && !hasKilledAllEnemies)
            {
                ShowRequiredEnemyKillCountMessage();
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
            GameService.Instance.uiService.ShowGameCompletion(GameString.GameCompletedText);
            SoundManager.Instance.soundService.StopSoundEffects();
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
            var PlayerView = GameService.Instance.playerService.PlayerController.PlayerView;
            PlayerView.ActivateDialoguePanel();
            PlayerView.ShowDialogue(GameString.DoNotHaveRequiredItem);
        }

        private void ShowRequiredEnemyKillCountMessage()
        {
            var PlayerView = GameService.Instance.playerService.PlayerController.PlayerView;
            PlayerView.ActivateDialoguePanel();
            PlayerView.ShowDialogue(GameString.DoNotHaveRequiredKillCount);
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