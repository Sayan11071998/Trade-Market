using UnityEngine;

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

        public void DeactivateBullet()
        {
            bulletModel.Deactivate();
            bulletView.SetActive(false);
            bulletPool.ReturnItem(this);
        }
    }
}