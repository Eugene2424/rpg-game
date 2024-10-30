using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;


public class Chapter1 : MonoBehaviour
{
    [SerializeField] private LocalizedString _mcName, _dialog;
    [SerializeField] private Animator _anim, _dialogBoxAnim;

    void Start()
    {
        _anim = GetComponent<Animator>();
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[2];
        StartCoroutine(Routine());
    }

    private void Update()
    {
        if (_dialogBoxAnim.GetBool("hide") && !_anim.GetBool("start"))
        {
            _anim.SetBool("start", true);
        }
    }
    

    IEnumerator Routine()
    {
        yield return new WaitForSeconds(2);
        UIManager.Instance.ShowDialogBox(_dialog.GetLocalizedString().Split('\n'), _mcName.GetLocalizedString());
    }
}
