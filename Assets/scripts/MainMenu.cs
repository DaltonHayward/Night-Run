using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Awake(){
        FindObjectOfType<AudioManager>().Stop("Upgrade Shop");
        FindObjectOfType<AudioManager>().Play("Main Menu");
    }

    public void PlayGame(){
        FindObjectOfType<AudioManager>().Stop("Main Menu");
        SceneManager.LoadScene("MainScene");
        SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("TileScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("CanvasScene", LoadSceneMode.Additive);
    }

    public void QuitGame(){
        // this won't happen in the unity inspector, it will work 
        // once you build the whole thing
        Application.Quit();
    }

    public void openMainMenu(){
        SceneManager.LoadScene("StartMenuScene");
    }

    public void open_store(){
        SceneManager.LoadScene("storeScene");
    }
}
