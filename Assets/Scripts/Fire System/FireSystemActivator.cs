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
                GameService.Instance.playerService.PlayerController.PlayerModel.EnableFire(true);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(GameString.PlayerTag))
                GameService.Instance.playerService.PlayerController.PlayerModel.EnableFire(false);
        }
    }
}