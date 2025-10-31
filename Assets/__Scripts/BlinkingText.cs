using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlinkingText : MonoBehaviour
{
    public TextMeshProUGUI uiText;               // Assign in Inspector or via code
    public float blinkSpeed = 1f;     // Speed of fade in/out

    private CanvasGroup canvasGroup;
    private bool fadingOut = true;

    void Start()
    {
        if (uiText == null)
        {
            uiText = GetComponent<TextMeshProUGUI>();
        }

        canvasGroup = uiText.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = uiText.gameObject.AddComponent<CanvasGroup>();
        }
    }

    void Update()
    {
        float direction = fadingOut ? -1f : 1f;
        canvasGroup.alpha += direction * blinkSpeed * Time.deltaTime;

        if (canvasGroup.alpha <= 0f)
        {
            canvasGroup.alpha = 0f;
            fadingOut = false;
        }
        else if (canvasGroup.alpha >= 1f)
        {
            canvasGroup.alpha = 1f;
            fadingOut = true;
        }
    }
}
