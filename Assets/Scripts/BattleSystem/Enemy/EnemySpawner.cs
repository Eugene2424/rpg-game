using System;
using System.Collections.Generic;
using PlasticPipe.PlasticProtocol.Messages;
using UnityEngine;
using Zenject;


namespace Game.BattleSystem
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] _availableSpots; 
        [SerializeField] private GameObject _enemyPrefab;

        private Stack<Vector3> _spots = new Stack<Vector3>();

        [Inject] 
        private EnemyPresenterFactory _enemyPresenterFactory;


        private void Start()
        {
            foreach (var spot in _availableSpots) { _spots.Push(spot.position); }
        }

        public EnemyPresenter SpawnEnemy(int maxHp, int atk)
        {
            var enemy = Instantiate(_enemyPrefab, _spots.Pop(), Quaternion.identity);
            var model = new EnemyModel(maxHp, atk);
            return _enemyPresenterFactory.Create(enemy.GetComponent<EnemyView>(), model);
        }
    }
}
