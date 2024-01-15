using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void RestartButton(){
        SceneManager.LoadScene("Game");
    }

    public void ExitButton(){
        SceneManager.LoadScene("MainMenu");
    }
}
