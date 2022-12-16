using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// The MainMenu
/// </summary>
public class MainMenu : MonoBehaviour 
{
    /// <summary>
    /// Start Game
    /// </summary>
    public void OnClick() {
        /// move Scene to Level 1
        SceneManager.LoadScene("Level1");
    }
    /// <summary>
    /// End Game
    /// </summary>
    public void QuitGame()
     {
        /// if in unity editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        /// if in exe file
        #else
            Application.Quit();
        #endif
     }
    /// <summary>
    /// Bace to Main Page
    /// </summary>
    public void Back()
    {
        SceneManager.LoadScene("StartPage");
    }
}
