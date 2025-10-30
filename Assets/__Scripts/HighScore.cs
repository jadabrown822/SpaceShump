using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    static private TextMeshProUGUI _UI_TEXT;
    static private int _SCORE = 1000;

    private TextMeshProUGUI txtCom;     // txtCom is a reference to this GO's Text component


    private void Awake()
    {
        _UI_TEXT = GetComponent<TextMeshProUGUI>();
    }


    static public int SCORE
    {
        get { return _SCORE; }
        private set
        {
            _SCORE = value;
            if (_UI_TEXT != null)
            {
                _UI_TEXT.text = "High Score: " + value.ToString("#,0");
            }
        }
    }


    static public void TRY_SET_HIGH_SCORE(int scoreToTry)
    {
        if (scoreToTry <= SCORE) return;
        SCORE = scoreToTry;
    }


    // the following code allow easy reset to the PlayerPref HighScore
    [Tooltip("Sheck this box to reset the HighScore in PlayerPrefs")]
    public bool resetHighScoreNow = false;

    private void OnDrawGizmos()
    {
        if (resetHighScoreNow)
        {
            resetHighScoreNow = false;
            PlayerPrefs.SetInt("HighScore", 1000);
            Debug.LogWarning("PlayerPrefs HighScore reset to 1,000.");
        }
    }


    /*
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    */
}
