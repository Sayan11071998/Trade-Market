using System.Collections.Generic;
using System.Linq;

namespace TradeMarket.EnemySystem
{
    public class EnemyRepository
    {
        private List<EnemyController> allEnemies;

        public EnemyRepository() => allEnemies = new List<EnemyController>();

        public void AddEnemy(EnemyController enemy) => allEnemies.Add(enemy);

        public List<EnemyController> GetAllEnemies() => new List<EnemyController>(allEnemies);

        public int GetAliveEnemiesCount() => allEnemies.Count(enemy => enemy.EnemyModel != null && !enemy.EnemyModel.IsDead);
    }
}