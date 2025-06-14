using UnityEngine;
using TradeMarket.Utilities;
using TradeMarket.EnemySystem;

namespace TradeMarket.BulletSystem
{
    public class PlayerBullet : BulletView
    {
        protected override void HandleTrigger(Collider2D other)
        {
            BulletController bulletController = GetController();
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