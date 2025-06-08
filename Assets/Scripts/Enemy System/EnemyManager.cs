using System.Collections.Generic;
using UnityEngine;

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

        public void EnableAllEnemiesFiring()
        {
            var enemies = enemyRepository.GetAllEnemies();
            foreach (var enemy in enemies)
                enemy.EnableFire();
            Debug.Log($"Enabled firing for {enemies.Count} enemies");
        }

        public void DisableAllEnemiesFiring()
        {
            var enemies = enemyRepository.GetAllEnemies();
            foreach (var enemy in enemies)
                enemy.DisableFire();
            Debug.Log($"Disabled firing for {enemies.Count} enemies");
        }
    }
}