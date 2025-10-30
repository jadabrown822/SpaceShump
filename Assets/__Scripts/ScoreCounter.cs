using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    static public ScoreCounter S { get; private set; }

    [Header("Dynamic")]
    public int score = 0;

    private TextMeshProUGUI uiText;


    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("ScoreCounter.Awake()");
        }

        uiText = GetComponent<TextMeshProUGUI>();
    }


    public void AddScore(int newScore)
    {
        score += newScore;
    }


    // Start is called before the first frame update
    void Start()
    {
        uiText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        uiText.text = score.ToString("#,0");
    }
}
