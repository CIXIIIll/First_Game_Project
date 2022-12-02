using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour 
{
    public void OnClick() {
        Debug.Log("Clicked");
        SceneManager.LoadScene("Level1");
    }
}
