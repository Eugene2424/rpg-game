using UnityEngine;
using System;
using System.Collections;
using DG.Tweening;
using Game.Interfaces;

public interface IPlayerView
{
    // Animation
    public void ShowAttackAnimation(IMoveable target, Action reachedAction = null);
    public void ShowMagicAnimation();
    public void ShowHitAnimation();
    public void ShowDeadAnimation();

    // Effects

    public void ShowHealingEffect();
    public void ShowManaEffect();
    public void ShowFireEffect(Enemy enemy);
    public void ShowPoisonEffect(int duration);
    public void ShowShieldEffect();
    public void ShowStrengthEffect();

    public void HideShieldEffect();
    public void HideStrengthEffect();
    public void HidePoisonEffect();

    // Sound
    public void PlayAttackSound();
    public void PlayHealingSound();
    public void PlayManaPotionSound();
    public void PlayStrengthPotionSound();
    public void PlayFireMagicSound();
    public void PlayShieldMagicSound();

    // Other
    public void SetMaxHP(int hp);
    public void SetMaxMP(int mp);
    public void SetHP(int hp);
    public void SetMP(int mp);
    public Vector3 GetStartPosition();
    public void ReachTargetAndGoBack(IMoveable target, Action reachedAction = null);
}

