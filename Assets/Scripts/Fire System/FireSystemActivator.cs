using UnityEngine;
using TradeMarket.Core;
using TradeMarket.Utilities;

namespace TradeMarket.FireSystem
{
    public class FireSystemActivator : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(GameString.PlayerTag))
            {
                var playerController = GameService.Instance.playerService.PlayerController;
                playerController.PlayerView.ActivateDialoguePanel();
                playerController.PlayerView.ShowDialogue(GameString.FireActivationDialogue);
                playerController.EnableFire();
                GameService.Instance.enemyManager.EnableAllEnemiesFiring();
                Debug.Log("Fire system activated! Enemies will start firing!");
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(GameString.PlayerTag))
            {
                GameService.Instance.playerService.PlayerController.DisableFire();
                GameService.Instance.enemyManager.DisableAllEnemiesFiring();
                Debug.Log("Fire system deactivated! Enemies stopped firing!");
            }
        }
    }
}