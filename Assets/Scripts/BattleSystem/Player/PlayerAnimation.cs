using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void PlayAttackAnimation()
    {
        _anim.SetTrigger("Attack");
    }

    public void PlayMagicAnimation()
    {
        _anim.SetTrigger("Magic");
    }

    public void PlayHitAnimation()
    {
        _anim.SetTrigger("Hit");
    }

    public void PlayDeadAnimation()
    {
        _anim.SetTrigger("Dead");
    }
}
