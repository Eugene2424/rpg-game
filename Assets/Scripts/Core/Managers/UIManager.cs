using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public bool dialogActive {  get; private set; }

    private typewriterUI _typeWriter;
    private Animator _anim;

    private string[] _dialogues;
    private string _characterName;
    private int _dialogCount = 0;

    void Awake()
    {
        if (Instance != null) 
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GameObject dialogBox = GameObject.Find("DialogBox");
        _typeWriter = dialogBox.GetComponentInChildren<typewriterUI>();
        _anim = dialogBox.GetComponent<Animator>();
    }

    private void Update()
    {
        if (dialogActive)
        {
            if (Input.GetButtonUp("Jump"))
            {
                if (_dialogCount < _dialogues.Length)
                    _typeWriter.TypeWrite(_characterName, _dialogues[_dialogCount]);
                _dialogCount++;
            }
            if (_dialogCount - 1 == _dialogues.Length)
            {
                _dialogCount++;
                StartCoroutine(HideDialogBox());
            }
        }
        
    }

    public void ShowDialogBox(string[] dialogues, string characterName)
    {
        _dialogues = dialogues;
        _characterName = characterName;
        dialogActive = true;
        _anim.SetBool("show", true);
        _anim.SetBool("hide", false);
        _typeWriter.TypeWrite(_characterName, _dialogues[0]);
        _dialogCount++;
    }

    IEnumerator HideDialogBox()
    {
        _anim.SetBool("show", false);
        _anim.SetBool("hide", true);
        yield return new WaitForSeconds(2.2f);
        dialogActive = false;
        _dialogues = null;
        _dialogCount = 0;
    }
}
