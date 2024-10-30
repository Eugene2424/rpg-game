using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Mushroom : Enemy
{
    [SerializeField] private int _poisonDuration;
    [SerializeField, Range(0, 100)] private int _poisonDamagePercent;
    private int _actionNum = 0;
    public override void Action(Player player)
    {
        if (IsDead == false)
        {
            _actionNum++;
            if (_actionNum < 3)
            {
                StartCoroutine(AttackRoutine(player));
            }
            else
            {
                _actionNum = 0;
                StartCoroutine(AttackRoutine(player));
                player.ApplyPoisonEffect(_poisonDuration, _poisonDamagePercent);
            }
        }
    }
}
