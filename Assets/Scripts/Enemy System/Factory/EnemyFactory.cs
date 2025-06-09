namespace TradeMarket.EnemySystem
{
    public class EnemyFactory
    {
        public static EnemyController CreateEnemy(EnemyView enemyView, EnemyScriptableObject enemyData)
        {
            var enemyModel = new EnemyModel(enemyData);
            var enemyController = new EnemyController(enemyModel, enemyView);

            enemyView.SetController(enemyController);
            enemyView.Initialize(enemyData);

            return enemyController;
        }
    }
}