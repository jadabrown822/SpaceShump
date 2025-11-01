using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuScreen : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("__Scene_0");
    }
}