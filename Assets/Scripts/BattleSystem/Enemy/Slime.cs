using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    private int _actionNum = 0;
    public override void Action(Player player)
    {
        _actionNum++;
        if (_actionNum == 1)
        {
            StartCoroutine(AttackRoutine(player));
        }
        else
        {
            _actionNum = 0;
        }
    }
}
