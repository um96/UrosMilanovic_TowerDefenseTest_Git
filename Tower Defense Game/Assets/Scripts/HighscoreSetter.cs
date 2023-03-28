using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreSetter : MonoBehaviour
{
    // Start is called before the first frame update
    Text _text;
    void Start()
    {
        _text = GetComponent<Text>();
        _text.text = (PlayerPrefs.GetInt("Highscore", 0)).ToString();
        if(_text.text == "0")
        {
            _text.text = "None!";
        }
    }
}
