using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem _deadEffect;


    public void ShowDead()
    {
        Destroy(Instantiate(_deadEffect, transform.position, _deadEffect.transform.rotation), 3);
    }
}
