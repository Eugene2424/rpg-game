using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Game.BattleSystem;

public class BattleInstaller : MonoInstaller
{
    public override void InstallBindings()
    {

        Container.BindInterfacesAndSelfTo<PlayerModel>()
            .AsSingle()
            .NonLazy();

        Container.Bind<IPlayerView>()
            .To<PlayerView>()
            .FromComponentInHierarchy()
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<PlayerPresenter>()
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<EnemyPresenterFactory>().AsSingle();
        
    }
}
