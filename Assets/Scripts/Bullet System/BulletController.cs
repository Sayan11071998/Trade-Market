using UnityEngine;
using TradeMarket.Utilities;
using TradeMarket.EnemySystem;
using TradeMarket.Core;

namespace TradeMarket.BulletSystem
{
    public class BulletController
    {
        private BulletModel bulletModel;
        private BulletView bulletView;
        private BulletPool bulletPool;

        public BulletModel BulletModel => bulletModel;
        public BulletView BulletView => bulletView;
        public bool IsActive => bulletModel.IsActive;

        public BulletController(BulletModel modelToSet, BulletView viewToSet, BulletPool bulletPoolToSet)
        {
            bulletModel = modelToSet;
            bulletView = viewToSet;
            bulletPool = bulletPoolToSet;

            bulletView.Initialize(this);
        }

        public void FireBullet(Vector2 startPosition, Vector2 direction)
        {
            bulletModel.Initialize(startPosition, direction);
            bulletView.SetActive(true);
            bulletView.SetPosition(startPosition);
            bulletView.SetVelocity(direction * bulletModel.Speed);
        }

        public void UpdateBullet()
        {
            if (!bulletModel.IsActive) return;

            bulletModel.UpdateLifetime(Time.deltaTime);

            if (!bulletModel.IsActive)
                DeactivateBullet();
        }

        public void OnTriggerDetected(Collider2D other)
        {
            if (!bulletModel.IsActive) return;

            if (bulletModel.bulletType == BulletType.Player)
            {
                if (other.CompareTag(GameString.EnemyTag))
                {
                    EnemyView enemyView = other.GetComponent<EnemyView>();
                    if (enemyView != null && enemyView.EnemyController != null)
                    {
                        enemyView.EnemyController.TakeDamage(bulletModel.Damage);
                        DeactivateBullet();
                    }
                }
                return;
            }

            if (bulletModel.bulletType == BulletType.Enemy)
            {
                if (other.CompareTag(GameString.PlayerTag))
                {
                    GameService.Instance.playerService.PlayerController.TakeDamage(bulletModel.Damage);
                    DeactivateBullet();
                }
                return;
            }
        }

        private void DeactivateBullet()
        {
            bulletModel.Deactivate();
            bulletView.SetActive(false);
            bulletPool.ReturnItem(this);
        }
    }
}