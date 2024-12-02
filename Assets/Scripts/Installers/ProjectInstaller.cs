using Game;
using Game.Configs;
using UnityEngine;
using Zenject;
using Game.Services;
using Game.Interfaces;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private IPlayerConfig _playerConfig;
    public override void InstallBindings()
    {
        //Configs
        Container.Bind<IPlayerConfig>().FromInstance(_playerConfig).AsSingle();
        
        // Services
        BindPlayerDataService();
    }

    private void BindPlayerDataService()
    {
        Container.Bind<IPlayerDataRepository>().To<JsonPlayerDataRepository>().AsSingle().NonLazy();
        Container.Bind<IPlayerDataService>().To<PlayerDataService>().AsSingle().NonLazy();
    }
}