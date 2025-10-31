using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    static private TextMeshProUGUI _UI_TEXT;
    static private int _SCORE;

    [Tooltip("Check this box to reset the HighScore in PlayerPrefs")]
    public bool resetHighScoreNow = false;

    private void Awake()
    {
        _UI_TEXT = GetComponent<TextMeshProUGUI>();
        _SCORE = PlayerPrefs.GetInt("HighScore", 1000);
        UpdateUIText();
    }

    static public int SCORE
    {
        get { return _SCORE; }
        private set
        {
            _SCORE = value;
            PlayerPrefs.SetInt("HighScore", _SCORE);
            PlayerPrefs.Save();
            UpdateUIText();
        }
    }

    static private void UpdateUIText()
    {
        if (_UI_TEXT != null)
        {
            _UI_TEXT.text = "High Score: " + _SCORE.ToString("#,0");
        }
    }

    static public void TRY_SET_HIGH_SCORE(int scoreToTry)
    {
        if (scoreToTry <= SCORE) return;
        SCORE = scoreToTry;
    }

#if UNITY_EDITOR
    void Update()
    {
        if (resetHighScoreNow)
        {
            resetHighScoreNow = false;
            PlayerPrefs.SetInt("HighScore", 1000);
            PlayerPrefs.Save();
            Debug.LogWarning("PlayerPrefs HighScore reset to 1,000.");
            _SCORE = 1000;
            UpdateUIText();
        }
    }
#endif
}
