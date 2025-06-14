using UnityEngine;
using TradeMarket.Utilities;
using TradeMarket.EnemySystem;

namespace TradeMarket.BulletSystem
{
    public class PlayerBullet : MonoBehaviour
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

            if (other.CompareTag(GameString.EnemyTag))
            {
                EnemyView enemyView = other.GetComponent<EnemyView>();
                if (enemyView != null && enemyView.EnemyController != null)
                {
                    enemyView.EnemyController.TakeDamage(bulletController.BulletModel.Damage);
                    bulletController.DeactivateBullet();
                }
            }
        }
    }
}