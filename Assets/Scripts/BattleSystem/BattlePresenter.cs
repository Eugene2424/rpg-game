using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.BattleSystem
{
    public class BattlePresenter
    {
        private PlayerPresenter _player;
        private EnemyPresenter[] _enemies;
        
        private BattleView _view;
        private BattleModel _model;

        public BattlePresenter(BattleView view, BattleModel model, PlayerPresenter player)
        {
            _view = view;
            _model = model;
        }
    }

}
