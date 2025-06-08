using UnityEngine;
using TradeMarket.Core;

namespace TradeMarket.EnemySystem
{
    public class EnemyController
    {
        private EnemyModel enemyModel;
        private EnemyView enemyView;

        public EnemyModel EnemyModel => enemyModel;
        public EnemyView EnemyView => enemyView;

        public EnemyController(EnemyModel enemyModelToSet, EnemyView enemyViewToSet)
        {
            enemyModel = enemyModelToSet;
            enemyView = enemyViewToSet;
        }

        public void EnableFire() => enemyModel.EnableFire(true);

        public void DisableFire() => enemyModel.EnableFire(false);

        public bool TryFire(out Vector2 fireDirection)
        {
            fireDirection = Vector2.zero;

            if (!enemyModel.CanFireNow())
                return false;

            Vector3 playerPosition = GetPlayerPosition();
            Vector3 enemyPosition = enemyView.transform.position;
            fireDirection = (playerPosition - enemyPosition).normalized;

            enemyModel.RegisterFire();
            return true;
        }

        private Vector3 GetPlayerPosition()
        {
            var playerController = GameService.Instance?.playerService?.PlayerController;
            if (playerController?.PlayerView != null)
                return playerController.PlayerView.transform.position;

            return Vector3.zero;
        }

        public void TakeDamage(float damageValue) => enemyModel.TakeDamage(damageValue);
    }
}