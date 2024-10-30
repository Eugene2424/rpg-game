using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.TextCore.Text;

public class StoryFontChanger : MonoBehaviour
{
    [SerializeField] private TMP_FontAsset _fontAsset;
    private TextMeshProUGUI _text;
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        if (DataManager.Instance.data.langId == 1)
            _text.font = _fontAsset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
