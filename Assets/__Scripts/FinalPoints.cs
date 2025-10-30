using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalPoints : MonoBehaviour
{
    static public FinalPoints S { get; private set; }

    [Header("Dynamic")]
    public int finalScore = 0;

    private TextMeshProUGUI pointText;


    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("GameOverScreen.Awake()");
        }

        pointText = GetComponent<TextMeshProUGUI>();

        finalScore = Main.finalScoreAtDeath;
    }


    private void Start()
    {
        pointText = GetComponent<TextMeshProUGUI>();
    }


    private void Update()
    {
        pointText.text = finalScore.ToString("#,0") + " POINTS";
    }
}
