using System;
using Zenject;


public class EnemyPresenterFactory : IFactory<IEnemyView, EnemyModel, EnemyPresenter>
{
    private readonly DiContainer _container;

    public EnemyPresenterFactory(DiContainer container)
    {
        _container = container;
    }

    public EnemyPresenter Create(IEnemyView view, EnemyModel model)
    {
        return _container.Instantiate<EnemyPresenter>( new object[] {view, model});
    }

}
