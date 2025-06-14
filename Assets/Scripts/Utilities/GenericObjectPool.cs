using System;
using System.Collections.Generic;

namespace TradeMarket.UISystem
{
    public class GenericObjectPool<T> where T : class
    {
        private List<PooledItem<T>> pooledItems = new List<PooledItem<T>>();

        protected T GetItem<U>() where U : T
        {
            if (pooledItems.Count > 0)
            {
                PooledItem<T> item = pooledItems.Find(item => !item.isUsed && item.Item is U);

                if (item != null)
                {
                    item.isUsed = true;
                    return item.Item;
                }
            }

            return CreateNewPooledItem<U>();
        }

        private T CreateNewPooledItem<U>() where U : T
        {
            PooledItem<T> newItem = new PooledItem<T>();

            newItem.Item = CreateItem<U>();
            newItem.isUsed = true;
            pooledItems.Add(newItem);

            return newItem.Item;
        }

        protected virtual T CreateItem<U>() where U : T
        {
            throw new NotImplementedException("Child class do not have implementation of CreateItem()");
        }

        public virtual void ReturnItem(T item)
        {
            PooledItem<T> pooledItem = pooledItems.Find(i => i.Item.Equals(item));
            pooledItem.isUsed = false;
        }

        public class PooledItem<TItem>
        {
            public TItem Item;
            public bool isUsed;
        }
    }
}