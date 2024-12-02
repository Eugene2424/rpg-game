using System;
using System.Collections;
using UnityEngine;
using Game.Interfaces;
using Zenject;


public class EnemyPresenter : IDisposable, IInitializable, IDamageable, IAttackable
{
    public EnemyModel Model;
    public IEnemyView View;

    public EnemyPresenter(EnemyModel model, IEnemyView view)
    {
        Model = model;
        View = view;

        Model.OnDeath += Dead;
    }

    public void Initialize()
    {
        View.SetMaxHP(Model.MaxHp);
        View.SetHP(Model.CurrentHp);
    }

    public void Dispose()
    {
        Model.OnDeath -= Dead;
    }

    public void SelectThis()
    {
        Model.Select();
        View.ShowIndicator();
    }

    public void DeselectThis()
    {
        Model.Deselect();
        View.HideIndicator();
    }

    public void Dead()
    {
        Model.Deselect();
        View.ShowThatDead();
    }

    public void Attack<T>(T target) where T : IDamageable, IMoveable
    {
        View.ShowAttackAnimation(target, () =>
        {
            target.GetHit(Model.Atk);
        });
    }
    
    
    public Vector3 GetPosition() => View.GetStartPosition();
    
    public void GetHit(int damage)
    {
        View.ShowHitAnimation();
        Model.DecreaseHealth(damage);
    }

}
