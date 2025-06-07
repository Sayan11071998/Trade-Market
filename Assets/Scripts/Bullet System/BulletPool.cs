using UnityEngine;
using TradeMarket.UISystem;

namespace TradeMarket.BulletSystem
{
    public class BulletPool : GenericObjectPool<BulletController>
    {
        protected BulletView bulletPrefab;
        protected BulletScriptableObject bulletData;
        protected Transform parentTransform;

        public BulletPool(BulletView bulletPrefabToSet, BulletScriptableObject bulletDataToSet, Transform parent = null)
        {
            bulletPrefab = bulletPrefabToSet;
            bulletData = bulletDataToSet;
            parentTransform = parent;
        }

        public BulletController GetBullet() => GetItem<BulletController>();

        protected override BulletController CreateItem<T>()
        {
            BulletView view = Object.Instantiate(bulletPrefab, parentTransform);
            BulletModel model = new BulletModel(bulletData);
            return new BulletController(model, view, this);
        }

        public override void ReturnItem(BulletController item)
        {
            base.ReturnItem(item);
        }
    }
}