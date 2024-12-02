using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class StatsWindow : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI hpStatText, mpStatText, defStatText, strStatText, magStatText, levelText;

    private void OnEnable()
    {
        hpStatText.text = "Max HP: " + DataManager.Instance.data.MaxHp;
        mpStatText.text = "Max MP: " + DataManager.Instance.data.MaxMp;
        defStatText.text = "DEF: " + DataManager.Instance.data.Def;
        strStatText.text = "STR: " + DataManager.Instance.data.Str;
        magStatText.text = "MAG: " + DataManager.Instance.data.Str;
        levelText.text = "Level " + DataManager.Instance.data.Level;
    }

    public void Show(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void Hide(GameObject obj)
    {
        obj.SetActive(false);
    }
}
