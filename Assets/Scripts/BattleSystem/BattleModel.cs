using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.BattleSystem
{
    public class BattleModel
    {
        
        private bool isActionCompleted = false, isBattleGoing = true;
        private EnemyPresenter[] _enemies;
        public PlayerPresenter player;
    
        public BattleModel(PlayerPresenter player)
        {
            this.player = player;
        }
    
        public EnemyPresenter GetSelectedEnemy()
        {
            foreach (EnemyPresenter enemy in _enemies)
            {
                if (enemy.Model.IsSelectedByPlayer)
                    return enemy;
            }
            return null;
        }
    
        public bool AreEnemiesDead()
        {
            int count = 0;
            foreach (var enemy in _enemies)
            {
                if (!enemy.Model.IsDead)
                    count++;
            }
            return _enemies.Length == count;
        }
    
        private int GainExpFromEnemies(EnemyPresenter[] enemies)
        {
            int totalExp = 0;
            foreach (EnemyPresenter enemy in enemies)
            {
                if (enemy.Model.IsDead)
                {
                    
                }
    
            }
            return totalExp;
        }
    }
    
}
