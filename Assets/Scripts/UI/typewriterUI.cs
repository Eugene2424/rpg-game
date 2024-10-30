using System.Collections;
using UnityEngine;
using TMPro;

public class typewriterUI : MonoBehaviour
{
    TMP_Text _tmpProText;
    string writer;
    private string _characterName = "";

    [SerializeField] float delayBeforeStart = 0f;
    [SerializeField] float timeBtwChars = 0.1f;
    [SerializeField] string leadingChar = "";

    // Use this for initialization
    void Start()
    {
        _tmpProText = GetComponent<TMP_Text>()!;

        if (_tmpProText != null)
        {
            writer = _tmpProText.text;
            _tmpProText.text = "";

            // If you want something to start typing on Start
            // StartCoroutine("TypeWriterTMP");
        }
    }

    // Call this method to start the typewriter with the character's name
    public void TypeWrite(string characterName, string textToWrite)
    {
        writer = textToWrite;
        _characterName = characterName;
        _tmpProText.text = _characterName + ": ";  // Name appears instantly with a colon
        StartCoroutine("TypeWriterTMP");
    }

    IEnumerator TypeWriterTMP()
    {
        yield return new WaitForSeconds(delayBeforeStart);

        // Type out the rest of the text (after the character's name)
        foreach (char c in writer)
        {
            if (_tmpProText.text.Length > 0)
            {
                _tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
            }
            _tmpProText.text += c;
            _tmpProText.text += leadingChar;
            yield return new WaitForSeconds(timeBtwChars);
        }

        if (leadingChar != "")
        {
            _tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
        }
    }
}
