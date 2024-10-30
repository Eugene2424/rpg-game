using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushMonster : Enemy
{
    [SerializeField] private GameObject _strEffect;
    [SerializeField, Range(0, 200)] private int _atkUpPercent;

    private GameObject _strAura;
    private int _actionNum = 0, _strEffectDuration = 2, _startAtk, _strNum;

    

    public override void Action(Player player)
    {
        if (IsDead == false)
        {
            _actionNum++;
            if (_actionNum < 4)
            {
                StartCoroutine(AttackRoutine(player));

                if (_strAura != null)
                {
                    _strNum++;
                    if (_strNum == _strEffectDuration + 1)
                        ClearStrEffect();
                }
            }
            else
            {
                _actionNum = 0;
                ApplyStrEffect();
            }
        }
    }

    private void ApplyStrEffect()
    {
        _startAtk = Atk;
        _strAura = Instantiate(_strEffect, transform.position, _strEffect.transform.rotation, transform);
        Atk += Atk * (_atkUpPercent / 100);
    }

    private void ClearStrEffect()
    {
        Atk = _startAtk;
        Destroy(_strAura);
    }
}
