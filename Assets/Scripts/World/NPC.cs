using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [SerializeField] private string[] _dialogues;
    [SerializeField] private string _characterName;

    private bool _isPlayerNearby = false;
    private Transform _focusObject;
    private CinemachineVirtualCamera _virtualCamera;

    private void Start()
    {
        _virtualCamera = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
        _focusObject = _virtualCamera.Follow;
    }

    private void Update()
    {
        if (Input.GetButtonUp("Jump") && _isPlayerNearby && !UIManager.Instance.dialogActive)
        {
            UIManager.Instance.ShowDialogBox(_dialogues, _characterName);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            _isPlayerNearby = true;
            _virtualCamera.Follow = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            _isPlayerNearby = false;
            _virtualCamera.Follow = _focusObject;
        }
    }
}
