using System;
using UnityEngine;



public class EnemyModel : IDisposable
{
    public event Action OnHealthChanged, OnDeath; 
    public int MaxHp { get; private set; }
    public int CurrentHp { get; private set; }
    public int Atk { get; private set; }
   
    public bool IsDead { get; private set; }
    public bool IsSelectedByPlayer { get; private set; }

    public EnemyModel(int maxHp, int atk)
    {
        MaxHp = maxHp;
        CurrentHp = MaxHp;
        Atk = atk;
    }

    public void Dispose()
    {
        OnDeath -= Dead;
    }
    
    public void Select()
    {
        IsSelectedByPlayer = true;
    }

    public void Deselect()
    {
        IsSelectedByPlayer = false;
    }

    public void IncreaseHealth(int value)
    {
        CurrentHp += value;
        UpdateHealth();
    }

    public void DecreaseHealth(int value)
    {
        CurrentHp -= value;
        UpdateHealth();
    }
    
    private void UpdateHealth()
    {
        if (CurrentHp <= 0)
            Dead();
        else if (CurrentHp > MaxHp)
            CurrentHp = MaxHp;

        OnHealthChanged?.Invoke();
    }

    private void Dead()
    {
        IsDead = true;
        OnDeath?.Invoke();
    }
}
