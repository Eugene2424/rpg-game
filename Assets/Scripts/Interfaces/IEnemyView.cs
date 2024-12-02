using System;
using System.Collections;
using Game.Interfaces;
using UnityEngine;

public interface IEnemyView
{
    public event Action ClickedOn;
    public void SetMaxHP(int maxHp);
    public void SetHP(int hp);

    public void HideHealthBar();
    public void ShowHealthBar();

    public void ShowIndicator();
    public void HideIndicator();

    public void ShowDeadAnimation();
    public void ShowAttackAnimation(IMoveable target, Action reachedAction = null);
    public void ShowHitAnimation();

    public void PlayAttackSound();

    public void ShowDeadEffect();

    public void ShowThatDead();
    public void ReachTargetAndGoBack(IMoveable target, Action reachedAction = null);
    public void StartRoutine(IEnumerator routine);
    public Vector3 GetStartPosition();

}
