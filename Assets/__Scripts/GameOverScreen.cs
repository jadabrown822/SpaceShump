using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{ 
    public void RestartButton()
    {
        SceneManager.LoadScene("__Scene_0");
    }


    public void ExitButton()
    {
        SceneManager.LoadScene("Main_Menu");
    }

}
