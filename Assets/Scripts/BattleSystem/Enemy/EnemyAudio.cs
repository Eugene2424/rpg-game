using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _attackSfx;
    private AudioSource _audioManager;


    void Start()
    {
        _audioManager = GetComponent<AudioSource>();
    }

    public void PlayAttackSfx()
    {
        _audioManager.clip = _attackSfx;
        _audioManager.Play();
    }
}
