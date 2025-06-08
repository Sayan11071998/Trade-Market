using System.Collections.Generic;

namespace TradeMarket.EnemySystem
{
    [System.Serializable]
    public class EnemySetup
    {
        public EnemyView enemyView;
        public EnemyScriptableObject enemyData;
    }

    public class EnemyManager
    {
        private EnemyRepository enemyRepository;

        public EnemyManager(List<EnemySetup> enemySetups)
        {
            enemyRepository = new EnemyRepository();
            InitializeEnemies(enemySetups);
        }

        private void InitializeEnemies(List<EnemySetup> enemySetups)
        {
            foreach (var enemySetup in enemySetups)
            {
                if (enemySetup.enemyView == null || enemySetup.enemyData == null) continue;

                EnemyController enemyController = EnemyFactory.CreateEnemy(enemySetup.enemyView, enemySetup.enemyData);
                enemyRepository.AddEnemy(enemyController);
            }
        }

        public List<EnemyController> GetAllEnemies() => enemyRepository.GetAllEnemies();

        public int GetEnemyCount() => enemyRepository.GetEnemyCount();
    }
}