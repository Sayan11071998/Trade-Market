using UnityEngine;
using TradeMarket.Utilities;
using TradeMarket.Core;

namespace TradeMarket.BulletSystem
{
    public class EnemyBullet : BulletView
    {
        protected override void HandleTrigger(Collider2D other)
        {
            BulletController bulletController = GetController();
            if (bulletController == null || !bulletController.IsActive) return;

            if (other.CompareTag(GameString.PlayerTag))
            {
                GameService.Instance.playerService.PlayerController.TakeDamage(bulletController.BulletModel.Damage);
                bulletController.DeactivateBullet();
            }
        }
    }
}