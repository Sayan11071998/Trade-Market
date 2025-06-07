using System.Collections.Generic;
using UnityEngine;

namespace TradeMarket.BulletSystem
{
    public class BulletService
    {
        private BulletPool bulletPool;
        private List<BulletController> activeBullets;
        private BulletScriptableObject bulletData;
        private BulletView bulletPrefab;

        public BulletService(BulletView bulletPrefabToSet, BulletScriptableObject bulletDataToSet, Transform poolParent = null)
        {
            bulletPrefab = bulletPrefabToSet;
            bulletData = bulletDataToSet;
            activeBullets = new List<BulletController>();
            bulletPool = new BulletPool(bulletPrefab, bulletData, poolParent);
        }

        public void FireBullet(Vector2 startPosition, Vector2 direction)
        {
            BulletController bullet = bulletPool.GetBullet();
            bullet.FireBullet(startPosition, direction);
            
            if (!activeBullets.Contains(bullet))
            {
                activeBullets.Add(bullet);
            }
        }

        public void UpdateBullets()
        {
            for (int i = activeBullets.Count - 1; i >= 0; i--)
            {
                BulletController bullet = activeBullets[i];
                
                if (bullet.IsActive)
                {
                    bullet.UpdateBullet();
                }
                else
                {
                    activeBullets.RemoveAt(i);
                }
            }
        }

        public void DeactivateAllBullets()
        {
            foreach (var bullet in activeBullets)
            {
                bullet.BulletModel.Deactivate();
                bullet.BulletView.SetActive(false);
                bulletPool.ReturnItem(bullet);
            }
            activeBullets.Clear();
        }

        public int GetActiveBulletCount() => activeBullets.Count;
    }
}