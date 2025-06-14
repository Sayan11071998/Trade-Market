using UnityEngine;
using TradeMarket.Utilities;
using TradeMarket.Core;

namespace TradeMarket.BulletSystem
{
    public class EnemyBullet : MonoBehaviour
    {
        private BulletView bulletView;
        private BulletController bulletController;

        private void Awake()
        {
            bulletView = GetComponent<BulletView>();
        }

        private void Start()
        {
            if (bulletView != null)
                bulletController = bulletView.GetController();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (bulletController == null || !bulletController.IsActive) return;

            if (other.CompareTag(GameString.PlayerTag))
            {
                GameService.Instance.playerService.PlayerController.TakeDamage(bulletController.BulletModel.Damage);
                bulletController.DeactivateBullet();
            }
        }
    }
}