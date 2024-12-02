using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Game.Interfaces;


namespace Game.BattleSystem
{
    public class PlayerModel : IInitializable
    {
        public event Action HealthChanged, ManaChanged, OnDeath;
        public bool CanTakeAction = true, ShieldEnabled = false;
        public bool IsDead { get; private set; }
        public int MaxHP { get; private set; }
        public int MaxMP { get; private set; }
        public int CurrentHp, CurrentMp, Str, Def;
        
        private readonly IPlayerDataService _playerDataService;
    
        public PlayerModel(IPlayerDataService playerDataService)
        {
            _playerDataService = playerDataService;
        }
    
        public void Initialize()
        {
            var data = _playerDataService.GetData();
            
            MaxHP = data.MaxHp;
            MaxMP = data.MaxMp;
            Str = data.Str;
            Def = data.Def;
    
            CurrentHp = MaxHP;
            CurrentMp = MaxMP;
        }
    
        public void IncreaseHealthPercent(float percent)
        {
            CurrentHp += (int)(MaxHP * percent);
            UpdateHealth();
        }
    
        public void DecreaseHealthPercent(float percent)
        {
            CurrentHp -= (int)(MaxHP * percent);
            UpdateHealth();
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
        
        public void IncreaseManaPercent(float percent)
        {
            CurrentHp += (int)(MaxHP * percent);
            UpdateMana();
        }
    
        public void DecreaseManaPercent(float percent)
        {
            CurrentMp -= (int)(MaxMP * percent);
            UpdateMana();
        }
        
        public void IncreaseMana(int value)
        {
            CurrentMp += value;
            UpdateMana();
        }
    
        public void DecreaseMana(int value)
        {
            CurrentMp -= value;
            UpdateMana();
        }
    
        public bool IsEnoughManaToWaste(float percent)
        {
            return (CurrentMp - ((int)(MaxMP * percent))) >= 0;
        }
    
        private void UpdateHealth()
        {
            if (CurrentHp <= 0)
                Dead();
            else if (CurrentHp > MaxHP)
                CurrentHp = MaxHP;
    
            HealthChanged?.Invoke();
        }
    
        private void UpdateMana()
        {
            if (CurrentMp <= 0)
                CurrentMp = 0;
            else if (CurrentMp > MaxMP)
                CurrentMp = MaxMP;
    
            ManaChanged?.Invoke();
        }

        private void Dead()
        {
            OnDeath?.Invoke();
            IsDead = true;
        }
    }
}

