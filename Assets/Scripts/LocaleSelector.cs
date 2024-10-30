using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class LocaleSelector : MonoBehaviour
{

    private void Start()
    {
        if (DataManager.Instance.data.langId != -1)
        {
            StartCoroutine(SetLocale(DataManager.Instance.data.langId));
        }
    }

    public void ChangeLocale(int localeId)
    {
        DataManager.Instance.data.langId = localeId;
        DataManager.Instance.SaveData();

        StartCoroutine(SetLocale(localeId));     
    }

    IEnumerator SetLocale(int localeId)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeId];

        SceneManager.LoadScene(1);
    }
}
