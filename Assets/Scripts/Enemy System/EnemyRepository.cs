using System.Collections.Generic;

namespace TradeMarket.EnemySystem
{
    public class EnemyRepository
    {
        private List<EnemyController> allEnemies;

        public EnemyRepository() => allEnemies = new List<EnemyController>();

        public void AddEnemy(EnemyController enemy) => allEnemies.Add(enemy);

        public List<EnemyController> GetAllEnemies() => new List<EnemyController>(allEnemies);
    }
}