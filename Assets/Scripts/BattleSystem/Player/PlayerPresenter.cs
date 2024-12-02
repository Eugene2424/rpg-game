using System.Collections;
using System.Collections.Generic;
using System;
using Game.Interfaces;
using UnityEngine;
using Zenject;
using DG.Tweening;


namespace Game.BattleSystem
{
    public class PlayerPresenter : IDisposable, IInitializable, IPlayer
    {
        public readonly PlayerModel Model;
        public readonly IPlayerView View;

        private float _poisonDamagePercent = 0;


        public PlayerPresenter(PlayerModel model, IPlayerView view)
        {
            Model = model;
            View = view;
 
            Model.HealthChanged += OnHealthChanged;
            Model.ManaChanged += OnManaChanged;
            Model.OnDeath += View.ShowDeadAnimation;
        }

        public void Initialize()
        {
            View.SetMaxHP(Model.MaxHP);
            View.SetMaxMP(Model.MaxMP);

            View.SetHP(Model.MaxHP);
            View.SetMP(Model.MaxMP);
        }

        public void Dispose()
        {
            Model.HealthChanged -= OnHealthChanged;
            Model.ManaChanged -= OnManaChanged;
            Model.OnDeath -= Dead;
        }
        
        public Vector3 GetPosition() => View.GetStartPosition();
        
        public void Attack<T>(T target) where T : IDamageable, IMoveable
        {
            View.ShowAttackAnimation(target, () =>
            {
                target.GetHit(Model.Str);
                View.PlayAttackSound();
            });
        }
        
        public void GetHit(int damage)
        {
            View.ShowHitAnimation();
            if (damage - Model.Def > 0)
                Model.DecreaseHealth(damage - Model.Def);
        }
        
        public void Heal(int amount)
        { 
            View.HidePoisonEffect(); 
            Model.IncreaseHealth(amount); 
            View.PlayHealingSound(); 
            View.ShowMagicAnimation();
            View.ShowHealingEffect();
        }
        
        
        public IEnumerator FireRoutine(Enemy enemy)
        {
            if (Model.IsEnoughManaToWaste(0.45f))
            {
                Model.DecreaseManaPercent(0.45f);

                View.PlayFireMagicSound();
                View.ShowMagicAnimation();
                View.ShowFireEffect(enemy);

                enemy.GetHit(DataManager.Instance.data.Mag);
                yield return new WaitForSeconds(2);
            }
        }

        public IEnumerator ShieldRoutine()
        {
            if (Model.IsEnoughManaToWaste(0.3f))
            {
                Model.DecreaseManaPercent(0.3f);
                Model.Def = DataManager.Instance.data.Def;

                View.PlayShieldMagicSound();
                View.ShowShieldEffect();
                View.ShowMagicAnimation();
                yield return new WaitForSeconds(1);
            }
        }

        public IEnumerator UsePotionRoutine(string potionType)
        {
            if (potionType == "HP" && DataManager.Instance.data.hpPotions > 0)
            {
                View.HidePoisonEffect();
                Model.IncreaseHealthPercent(0.3f);
                DataManager.Instance.UsedPotion(potionType);

                View.PlayHealingSound();
                View.ShowMagicAnimation();
                View.ShowHealingEffect();
                yield return new WaitForSeconds(2);
            }

            else if (potionType == "MP" && DataManager.Instance.data.mpPotions > 0)
            {
                Model.IncreaseManaPercent(0.3f);
                DataManager.Instance.UsedPotion(potionType);

                View.PlayManaPotionSound();
                View.ShowMagicAnimation();
                View.ShowManaEffect();
                yield return new WaitForSeconds(2);
            }

            else if (potionType == "STR" && DataManager.Instance.data.strPotions > 0)
            {
                Model.Str += (int)(DataManager.Instance.data.Str * 0.3f);
                DataManager.Instance.UsedPotion(potionType);

                View.PlayStrengthPotionSound();
                View.ShowMagicAnimation();
                View.ShowStrengthEffect();
                yield return new WaitForSeconds(1);
            }
        }

        public void ApplyPoisonEffect(int duration, float damagePercent)
        {
            _poisonDamagePercent = damagePercent;
            View.ShowPoisonEffect(duration);
        }
        

        

        public void Dead()
        {
            View.ShowDeadAnimation();
        }
        
        private void OnHealthChanged()
        {
            View.SetHP(Model.CurrentHp);
        }

        private void OnManaChanged()
        {
            View.SetMP(Model.CurrentMp);
        }

    }
}