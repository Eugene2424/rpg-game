using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip attackSfx;
    [SerializeField] private AudioClip healingSfx;
    [SerializeField] private AudioClip mpPotionSfx;
    [SerializeField] private AudioClip strPotionSfx;
    [SerializeField] private AudioClip fireMagicSfx;
    [SerializeField] private AudioClip shieldMagicSfx;

    private AudioSource _audio;

    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void PlayAttackSound()
    {
        _audio.PlayOneShot(attackSfx);
    }

    public void PlayHealingSound()
    {
        _audio.PlayOneShot(healingSfx);
    }

    public void PlayManaPotionSound()
    {
        _audio.PlayOneShot(mpPotionSfx);
    }

    public void PlayStrengthPotionSound()
    {
        _audio.PlayOneShot(strPotionSfx);
    }

    public void PlayFireMagicSound()
    {
        _audio.PlayOneShot(fireMagicSfx);
    }

    public void PlayShieldMagicSound()
    {
        _audio.PlayOneShot(shieldMagicSfx);
    }
}
