using System;
using System.Collections;
using Game.Interfaces;
using UnityEngine;
using DG.Tweening;


namespace Game.BattleSystem
{
    [RequireComponent(typeof(EnemyAnimation), typeof(EnemyAudio), typeof(EnemyEffects))]
    public class EnemyView : MonoBehaviour, IEnemyView
    {
        public event Action ClickedOn;
        private HealthBar _healthBar;
        private GameObject _selectedIndicator;

        private EnemyAnimation _anim;
        private EnemyAudio _audio;
        private EnemyEffects _effects;

        private Vector3 _startPos;

        private void Start()
        {
            _anim = GetComponent<EnemyAnimation>();
            _audio = GetComponent<EnemyAudio>();
            _effects = GetComponent<EnemyEffects>();

            _healthBar = transform.GetChild(1).GetComponent<HealthBar>();
            _selectedIndicator = transform.GetChild(0).gameObject;

            _startPos = transform.position;
        }

        public void SetMaxHP(int maxHp)
        {
            _healthBar.maxValue = maxHp;
        }

        public void SetHP(int hp)
        {
            _healthBar.value = hp;
        }

        // Animation
        public void ShowAttackAnimation(IMoveable target, Action reachedAction = null)
        {
            _anim.PlayAttack();
            ReachTargetAndGoBack(target, reachedAction);
        }

        public void ShowHitAnimation()
        {
            
            Sequence sequence = DOTween.Sequence();

            sequence.Append(MoveToPosition(GetStartPosition() + Vector3.left * 2, GameConstants.SHORT_TIME)
                .SetEase(Ease.Linear));

            sequence.AppendCallback(() => { _anim.PlayHit(); });
            
            sequence.Append(MoveToPosition(GetStartPosition(), GameConstants.SHORT_TIME)
                .SetEase(Ease.Linear));
        }

        public void ShowDeadAnimation()
        {
            _anim.PlayDead();
        }

        // Sound 
        public void PlayAttackSound()
        {
            _audio.PlayAttackSfx();
        }

        // Effects
        public void ShowDeadEffect()
        {
            _effects.ShowDead();
        }

        // Other
        public void HideHealthBar()
        {
            _healthBar.gameObject.SetActive(false);
        }

        public void ShowHealthBar()
        {
            _healthBar.gameObject.SetActive(true);
        }


        public void ShowIndicator()
        {
            _selectedIndicator.SetActive(false);
        }

        public void HideIndicator()
        {
            _selectedIndicator.SetActive(true);
        }


        private void OnMouseUp()
        {
            ClickedOn?.Invoke();
        }

        public void StartRoutine(IEnumerator routine)
        {
            StartCoroutine(routine);
        }

        public void ShowThatDead()
        {
            StartCoroutine(DeadRoutine());
        }

        IEnumerator DeadRoutine()
        {
            ShowDeadAnimation();
            yield return new WaitForSeconds(2);
            ShowDeadEffect();
            gameObject.SetActive(false);
        }
        
        public Vector3 GetStartPosition() => _startPos;

        public void ReachTargetAndGoBack(IMoveable target, Action reachedAction = null)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(MoveToPosition(GetStartPosition() - Vector3.right * 2, GameConstants.SHORT_TIME)
                .SetEase(Ease.Linear));
            sequence.Append(MoveToPosition(target.GetPosition(), GameConstants.SHORT_TIME)
                .SetEase(Ease.Linear));

            sequence.AppendCallback((() => { reachedAction?.Invoke(); }));
            
            sequence.Append(MoveToPosition(GetStartPosition(), GameConstants.SHORT_TIME)
                .SetEase(Ease.Linear));
        }

        private Tween MoveToPosition(Vector3 target, float time)
        {
            return transform.DOMove(target, time);
        }
    }
}

