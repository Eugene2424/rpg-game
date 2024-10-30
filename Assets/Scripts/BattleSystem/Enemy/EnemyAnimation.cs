using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator _anim;
    void Start()
    {
        _anim = GetComponent<Animator>();
    }
    public void PlayAttack()
    {
        _anim.SetTrigger("attack");
    }

    public void PlayHit()
    {
        _anim.SetTrigger("hit");
    }

    public void PlayDead()
    {
        _anim.SetBool("Dead", true);
    }
}
