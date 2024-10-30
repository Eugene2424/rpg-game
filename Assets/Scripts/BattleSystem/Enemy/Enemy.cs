using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField, Min(0)] private int _maxHp, _atk;

    private EnemyAnimation _anim;
    private EnemyAudio _audio;
    private EnemyEffects _effects;

    private HealthBar _hpBar;
    private GameObject _selectedIndicator;

    private int _hp;
    private Vector3 _startPos;

    public string enemyName;
    public bool IsSelectedByPlayer = false;
    public bool IsDead { get; set; }
    
    public int GetMaxHp() => _maxHp;
    public int Atk 
    {
        get { return _atk; }
        protected set { _atk = value; }
    }

    void Start()
    {
        _anim = GetComponent<EnemyAnimation>();
        _audio = GetComponent<EnemyAudio>();
        _effects = GetComponent<EnemyEffects>();

        _hpBar = transform.GetChild(1).GetComponent<HealthBar>();
        _selectedIndicator = transform.GetChild(0).gameObject;

        _hp = _maxHp;
        _hpBar.maxValue = _maxHp;
        _startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _hpBar.value = _hp;
        if (_hp <= 0)
            Dead();
    }


    public virtual void Action(Player player)
    {
        StartCoroutine(AttackRoutine(player));
    }

    protected IEnumerator AttackRoutine(Player player)
    {
        if (IsDead == false)
        {
            
            StartCoroutine(MoveToPosition(_startPos - Vector3.right * 2 + Vector3.up, 0.3f));
            _hpBar.gameObject.SetActive(false);
            _anim.PlayAttack();
            
            yield return new WaitForSeconds(0.3f);
            
            StartCoroutine(MoveToPosition(player.transform.position - Vector3.right * 2, 0.3f));
            
            yield return new WaitForSeconds(0.3f);
            
            player.GetHit(_atk);
            _audio.PlayAttackSfx();
            _hpBar.gameObject.SetActive(true);
            StartCoroutine(MoveToPosition(_startPos, 0.4f));
        }
        
    }


    private void OnMouseUp()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            enemy.Deselect();
        }
        _selectedIndicator.SetActive(true);
        IsSelectedByPlayer = true;
    }

    public void Deselect()
    {
        _selectedIndicator.SetActive(false);
        IsSelectedByPlayer = false;
    }

    public void GetHit(int damage)
    { 
        StartCoroutine(GetHitRoutine(damage));
    }

    IEnumerator GetHitRoutine(int damage)
    {
        StartCoroutine(MoveToPosition(_startPos - Vector3.right * 2, 0.2f));
        _anim.PlayHit();
        _hp = _hp - damage;

        yield return new WaitForSeconds(0.2f);

        StartCoroutine(MoveToPosition(_startPos, 0.2f));
    }

    public void Dead()
    {
        IsSelectedByPlayer = false;
        StartCoroutine(DeadRoutine());
    }

    IEnumerator DeadRoutine()
    {
        Deselect();
        _anim.PlayDead();
        IsDead = true;
        _hpBar.gameObject.SetActive(false);

        yield return new WaitForSeconds(2);

        _effects.ShowDead();
        gameObject.SetActive(false);
    }

    protected IEnumerator MoveToPosition(Vector3 target, float time)
    {
        Vector3 startPosition = transform.position;  // Current position as the start
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            // Lerp from the current position to the target position
            transform.position = Vector3.Lerp(startPosition, target, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the object reaches the exact target position at the end
        transform.position = target;
    }
}
