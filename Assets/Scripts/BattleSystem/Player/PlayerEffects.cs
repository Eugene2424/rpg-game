using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EffectType
{
    SHIELD_BUFF,
    STR_BUFF,
    POISON_EFFECT
}

public class PlayerEffects : MonoBehaviour
{

    [Header("VFX")]
    [SerializeField] private GameObject healingEffect;
    [SerializeField] private GameObject mpPotionEffect;
    [SerializeField] private GameObject strPotionEffect;
    [SerializeField] private GameObject fireEffect;
    [SerializeField] private GameObject shieldEffect;
    [SerializeField] private GameObject poisonEffect;

    private GameObject shield, strAura, poisonAura;

    private int[] effectsNums = new int[Enum.GetValues(typeof(EffectType)).Length];
    private int[] effectsDuration = new int[Enum.GetValues(typeof(EffectType)).Length];

    private void Start()
    {
        effectsDuration[(int)EffectType.SHIELD_BUFF] = 2;
        effectsDuration[(int)EffectType.STR_BUFF] = 2;
    }

    public void UpdateEffectsNums()
    {
        for (int i = 0; i < effectsNums.Length; i++)
        {
            if (effectsNums[i] != 0)
            {
                effectsNums[i]++;
            }

        }
    }


    public bool IsEffectEnded(EffectType effect)
    {
        return effectsNums[(int)effect] == effectsDuration[(int)effect] + 3;
    }

    public bool IsEffectActivated(EffectType effect)
    {
        return effectsNums[(int)effect] > 0;
    }


    public void PlayHealingEffect()
    {
        Destroy(Instantiate(healingEffect, transform.position, Quaternion.identity), 3);
    }

    public void PlayManaEffect()
    {
        Destroy(Instantiate(mpPotionEffect, transform.position, Quaternion.identity), 3);
    }

    public void PlayFireEffect(Enemy enemy)
    {
        Destroy(Instantiate(fireEffect, enemy.gameObject.transform.position, Quaternion.identity), 2);
    }



    public void ActivatePoisonEffect(int duration)
    {
        
        effectsNums[(int)EffectType.POISON_EFFECT] = 1;
        effectsDuration[(int)EffectType.POISON_EFFECT] = duration;
        poisonAura = Instantiate(poisonEffect, transform.position, poisonEffect.transform.rotation, transform);
    }

    public void ActivateShieldEffect()
    {
        effectsNums[(int)EffectType.STR_BUFF] = 1;
        shield = Instantiate(shieldEffect, transform.position, Quaternion.identity, transform);
    }

    public void ActivateStrengthEffect()
    {
        effectsNums[(int)EffectType.STR_BUFF] = 1;
        strAura = Instantiate(strPotionEffect, transform.position, Quaternion.identity, transform);
    }



    public void DestroyShieldEffect()
    {
        effectsNums[(int)EffectType.SHIELD_BUFF] = 0;
        Destroy(shield);
    }

    public void DestroyStrengthEffect()
    {
        effectsNums[(int)EffectType.STR_BUFF] = 0;
        Destroy(strAura);
    }

    public void DestroyPoisonEffect()
    {
        effectsNums[(int)EffectType.POISON_EFFECT] = 0;
        Destroy(poisonAura);
    }
}
