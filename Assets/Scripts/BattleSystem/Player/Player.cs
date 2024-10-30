using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



[RequireComponent(typeof(PlayerAnimation), typeof(PlayerAudio), typeof(PlayerEffects))]
public class Player : MonoBehaviour, IDamageable
{
 
    [Header("Progress Bars")]
    [SerializeField] private Slider _hpBar;
    [SerializeField] private Slider _mpBar;

    private PlayerAudio _audio;
    private PlayerEffects _effects;
    private PlayerAnimation _anim;

    private int _hp, _mp, _str, _def = 0;
    private float _poisonDamagePercent = 0;

    [HideInInspector] public bool CanTakeAction = true, ShieldEnabled = false;
    public bool IsDead { get; set; }

    private Vector3 _startPos;
 

    private void Start()
    { 
        _audio = GetComponent<PlayerAudio>();
        _anim = GetComponent<PlayerAnimation>();
        _effects = GetComponent<PlayerEffects>();

        _hp = DataManager.Instance.data.maxHp;
        _mp = DataManager.Instance.data.maxMp;
        _str = DataManager.Instance.data.str;

        _hpBar.maxValue = _hp;
        _mpBar.maxValue = _mp;
        _startPos = transform.position;
    }

    private void Update()
    {
        _hpBar.value = _hp;

        _mpBar.value = _mp;

        if (_hp <= 0)
            Dead();
        else if (_hp > _hpBar.maxValue)
            _hp = (int)_hpBar.maxValue;
        if (_mp > _mpBar.maxValue)
            _mp = (int)_mpBar.maxValue;


        if (_effects.IsEffectEnded(EffectType.STR_BUFF))
        {
            _str = DataManager.Instance.data.str;
        }
    }

    public void Action(IEnumerator action)
    {
        if (CanTakeAction && !IsDead)
        {
            CanTakeAction = false;
            StartCoroutine(action);
            _effects.UpdateEffectsNums();

            if (_effects.IsEffectActivated(EffectType.POISON_EFFECT))
            {
                _hp -= (int)(_hpBar.maxValue * (_poisonDamagePercent / 100f));
            }
            else if (_effects.IsEffectEnded(EffectType.POISON_EFFECT))
            {
                ClearPoisonEffect();
            }
        }
    }

    public IEnumerator HealRoutine()
    {
        if (_mp - (int)(_mpBar.maxValue * 0.4f) + DataManager.Instance.data.level >= 0)
        {
            ClearPoisonEffect();
            _hp += (int)(_hpBar.maxValue * 0.3f);
            _mp -= (int)(_mpBar.maxValue * 0.4f) + DataManager.Instance.data.level;

            _audio.PlayHealingSound();
            _anim.PlayMagicAnimation();
            _effects.PlayHealingEffect();
            yield return new WaitForSeconds(2);
        }
    }

    public IEnumerator FireRoutine(Enemy enemy)
    {
        if (_mp - (int)(_mpBar.maxValue * 0.45f) >= 0)
        {
            _mp -= (int)(_mpBar.maxValue * 0.45f);

            _audio.PlayFireMagicSound();
            _anim.PlayMagicAnimation();
            _effects.PlayFireEffect(enemy);

            enemy.GetHit(DataManager.Instance.data.mag);
            yield return new WaitForSeconds(2);
        }
    }

    public IEnumerator AttackRoutine(Enemy enemy)
    {
        StartCoroutine(MoveToPosition(enemy.transform.position + Vector3.right * 2 - Vector3.up, 1.2f));
        _anim.PlayAttackAnimation();
        yield return new WaitForSeconds(1.2f);
        enemy.GetHit(_str);
        _audio.PlayAttackSound();
        yield return new WaitForSeconds(1);
        StartCoroutine(MoveToPosition(_startPos, 0.3f));
    }

    public IEnumerator ShieldRoutine()
    {
        if (_mp - (int)(_mpBar.maxValue * 0.3f) >= 0)
        {
            _mp -= (int)(_mpBar.maxValue * 0.3f);
            _def = DataManager.Instance.data.def;

            _audio.PlayShieldMagicSound();
            _effects.ActivateShieldEffect();
            _anim.PlayMagicAnimation();
            yield return new WaitForSeconds(1);
        }
    }

    public IEnumerator UsePotionRoutine(string potionType)
    {
        if (potionType == "HP" && DataManager.Instance.data.hpPotions > 0)
        {
            ClearPoisonEffect();
            _hp += (int)(_hpBar.maxValue * 0.3f);
            DataManager.Instance.UsedPotion(potionType);

            _audio.PlayHealingSound();
            _anim.PlayMagicAnimation();
            _effects.PlayHealingEffect();
            yield return new WaitForSeconds(2);
        }

        else if (potionType == "MP" && DataManager.Instance.data.mpPotions > 0)
        {
            _mp += (int)(_hpBar.maxValue * 0.3f);
            DataManager.Instance.UsedPotion(potionType);

            _audio.PlayManaPotionSound();
            _anim.PlayMagicAnimation();
            _effects.PlayManaEffect();
            yield return new WaitForSeconds(2);
        }

        else if (potionType == "STR" && DataManager.Instance.data.strPotions > 0)
        {
            
            _str += (int)(DataManager.Instance.data.str * 0.3f);
            DataManager.Instance.UsedPotion(potionType);

            _audio.PlayStrengthPotionSound();
            _anim.PlayMagicAnimation();
            _effects.ActivateStrengthEffect();
            yield return new WaitForSeconds(1);
        }
    }

    public void ApplyPoisonEffect(int duration, float damagePercent)
    {
        _poisonDamagePercent = damagePercent;
        _effects.ActivatePoisonEffect(duration);
    }


    IEnumerator MoveToPosition(Vector3 target, float time)
    {
        Vector3 startPosition = transform.position;  // Current position as the start
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            // Lerp from the current position to the target position
            transform.position = Vector3.Lerp(startPosition, target, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the object reaches the exact target position at the end
        transform.position = target;
    }

    public void ClearShieldEffect()
    {
        _def = 0;
        _effects.DestroyShieldEffect();
    }

    private void ClearPoisonEffect()
    {
        _effects.DestroyPoisonEffect();
    }

    public void GetHit(int damage)
    {
        StartCoroutine(GetHitCoroutine(damage));
    }

    IEnumerator GetHitCoroutine(int damage)
    {
        StartCoroutine(MoveToPosition(_startPos + Vector3.right * 2, 0.2f));
        _anim.PlayHitAnimation();
        if (damage - _def > 0)
            _hp = _hp - (damage - _def);
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(MoveToPosition(_startPos, 0.2f));
    }

    public void Dead()
    {
        _anim.PlayDeadAnimation();
        IsDead = true;
    }

}
