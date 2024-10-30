using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Animator _anim;
    private Rigidbody2D _rb;
    private AudioSource _audioSource;

    private float _horizontalInput, _verticalInput;
    private bool _isMoved = false;


    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();

        if (WorldManager.Instance.savedPos[SceneManager.GetActiveScene().name] != Vector3.zero)
            transform.position = WorldManager.Instance.savedPos[SceneManager.GetActiveScene().name];
    }


    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        // if vertical and horizontal input not touched in same time
        if (!(Mathf.Abs(_horizontalInput) > 0 && Mathf.Abs(_verticalInput) > 0) && !UIManager.Instance.dialogActive)
        {
            if (Mathf.Abs(_horizontalInput + _verticalInput) > 0)
            {
                UpdateMovement();

                if (!_isMoved)
                    OnMovement();
                    _isMoved = true;

            }
            else
            {
                OnStopMovement();
                _isMoved = false;
            }

        }
        else
        {
            OnStopMovement();
        }
    }


    private void UpdateMovement()
    {
        _rb.velocity = new Vector2(_horizontalInput, _verticalInput) * _speed;

        _anim.SetBool("isMoving", true);
        _anim.SetFloat("horizontal", _horizontalInput);
        _anim.SetFloat("vertical", _verticalInput);
    }


    private void OnMovement()
    {
      
    }

    private void OnStopMovement()
    {
        _rb.velocity = Vector2.zero;

        _anim.SetBool("isMoving", false);
        _anim.SetFloat("horizontal", 0);
        _anim.SetFloat("vertical", 0);

        _audioSource.Stop();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        
        if (collision.collider.CompareTag("forest"))
        {
           // DataManager.Instance.posVillage = transform.position - Vector3.right * 3;
            SceneManager.LoadScene("Forest2");
        }

        if (collision.collider.CompareTag("village"))
        {
            SceneManager.LoadScene("GameScene");
        }
        if (collision.collider.CompareTag("cave"))
        {
            WorldManager.Instance.savedPos[SceneManager.GetActiveScene().name] = transform.position - Vector3.up * 2;
            SceneManager.LoadScene("Cave1");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            EnemyController enemy = collision.GetComponent<EnemyController>();
            enemy.PutDataForBattle();
            WorldManager.Instance.battleWindow.gameObject.SetActive(true);
            transform.position = WorldManager.Instance.savedPos[SceneManager.GetActiveScene().name];
        }
    }
}
