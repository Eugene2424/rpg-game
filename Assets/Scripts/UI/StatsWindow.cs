using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class StatsWindow : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI hpStatText, mpStatText, defStatText, strStatText, magStatText, levelText;

    private void OnEnable()
    {
        hpStatText.text = "Max HP: " + DataManager.Instance.data.maxHp;
        mpStatText.text = "Max MP: " + DataManager.Instance.data.maxMp;
        defStatText.text = "DEF: " + DataManager.Instance.data.def;
        strStatText.text = "STR: " + DataManager.Instance.data.str;
        magStatText.text = "MAG: " + DataManager.Instance.data.str;
        levelText.text = "Level " + DataManager.Instance.data.level;
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
