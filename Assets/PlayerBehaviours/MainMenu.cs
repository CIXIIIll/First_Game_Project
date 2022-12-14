using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour 
{
    public void OnClick() {
        SceneManager.LoadScene("Level1");
    }
    public void QuitGame()
     {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
     }
    public void Back()
    {
        SceneManager.LoadScene("StartPage");
    }
}
