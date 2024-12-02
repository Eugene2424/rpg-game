using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.BattleSystem
{
    [RequireComponent(typeof(PlayerAnimation), typeof(PlayerAudio), typeof(PlayerEffects))]
public class PlayerView : MonoBehaviour, IPlayerView
{
    [Header("Progress Bars")]
    [SerializeField] private Slider _hpBar;
    [SerializeField] private Slider _mpBar;

    private PlayerAudio _audio;
    private PlayerEffects _effects;
    private PlayerAnimation _anim;

    private Vector3 _startPosition;

    void Start()
    {
        _audio = GetComponent<PlayerAudio>();
        _anim = GetComponent<PlayerAnimation>();
        _effects = GetComponent<PlayerEffects>();

        _startPosition = transform.position;
    }

    // Animation

    public void ShowAttackAnimation(IMoveable target, Action reachedAction = null)
    {
        _anim.PlayAttackAnimation();
        ReachTargetAndGoBack(target, reachedAction);
    }

    public void ShowMagicAnimation()
    {
        _anim.PlayMagicAnimation();
    }

    public void ShowHitAnimation()
    {
        Sequence sequence = DOTween.Sequence();
            
        sequence.Append(MoveToPosition(GetStartPosition() + Vector3.right * 2, GameConstants.SHORT_TIME)
            .SetEase(Ease.Linear));

        sequence.AppendCallback(() =>
        {
            _anim.PlayHitAnimation();
        });

        sequence.AppendInterval(GameConstants.SHORT_TIME);
        sequence.Append(MoveToPosition(GetStartPosition(), GameConstants.SHORT_TIME)
            .SetEase(Ease.Linear));
    }

    public void ShowDeadAnimation()
    {
        _anim.PlayDeadAnimation();
    }

    // Effects

    public void ShowHealingEffect()
    {
        _effects.PlayHealingEffect();
    }

    public void ShowManaEffect()
    {
        _effects.PlayManaEffect();
    }

    public void ShowFireEffect(Enemy enemy)
    {
        _effects.PlayFireEffect(enemy);
    }

    public void ShowPoisonEffect(int duration)
    {
        _effects.ActivatePoisonEffect(duration);
    }

    public void ShowShieldEffect()
    {
        _effects.ActivateShieldEffect();
    }

    public void ShowStrengthEffect()
    {
        _effects.ActivateStrengthEffect();
    }

    public void HideShieldEffect()
    {
        _effects.DestroyShieldEffect();
    }

    public void HideStrengthEffect()
    {
        _effects.DestroyStrengthEffect();
    }

    public void HidePoisonEffect()
    {
        _effects.DestroyPoisonEffect();
    }

    // Audio

    public void PlayAttackSound()
    {
        _audio.PlayAttackSound();
    }

    public void PlayHealingSound()
    {
        _audio.PlayHealingSound();
    }

    public void PlayManaPotionSound()
    {
        _audio.PlayManaPotionSound();
    }

    public void PlayStrengthPotionSound()
    {
        _audio.PlayStrengthPotionSound();
    }

    public void PlayFireMagicSound()
    {
        _audio.PlayFireMagicSound();
    }

    public void PlayShieldMagicSound()
    {
        _audio.PlayShieldMagicSound();
    }

    // Other

    public void SetMaxHP(int hp)
    {
        _hpBar.maxValue = hp;
    }

    public void SetMaxMP(int mp)
    {
        _mpBar.maxValue = mp;
    }

    public void SetHP(int hp)
    {
        _hpBar.value = hp;
    }

    public void SetMP(int mp)
    {
        _mpBar.value = mp;
    }

    public void ReachTargetAndGoBack(IMoveable target, Action reachedAction = null)
    {
        Sequence sequence = DOTween.Sequence();
            
        sequence.Append(MoveToPosition(target.GetPosition(), GameConstants.ATTACK_MOVE_TIME).SetEase(Ease.Linear));
        sequence.AppendCallback(() =>
        {
            reachedAction?.Invoke();
        });
        sequence.AppendInterval(1f);
        sequence.Append(MoveToPosition(GetStartPosition(), GameConstants.SHORT_TIME).SetEase(Ease.Linear));
    }

    public Vector3 GetStartPosition()
    {
        return _startPosition;
    }

    public void StartRoutine(IEnumerator routine)
    {
        StartCoroutine(routine);
    }

    public Tween MoveToPosition(Vector3 target, float time)
    {
        return transform.DOMove(target, time);
    }
}
}

