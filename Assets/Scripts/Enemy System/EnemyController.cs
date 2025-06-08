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
    }
}