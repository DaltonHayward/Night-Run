using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void closePauseMenu(GameObject menu){
        Destroy(menu);
        Time.timeScale = 1;
    }

    public void refillHealth(GameObject PowerUpMenu){
        GameManager.instance.player.GetComponent<Player>().refillHealth();
        Destroy(PowerUpMenu);
        Time.timeScale = 1; 
        GameManager.instance.is_level_menu_open = 0;
    }

    public void increaseDamage(GameObject PowerUpMenu){
        GameManager.instance.bullet.GetComponent<Projectile>().increaseDamage(25.0f);
        Destroy(PowerUpMenu);
        Time.timeScale = 1;
        GameManager.instance.is_level_menu_open = 0;
    }

    public void giveShield(GameObject PowerUpMenu){
        GameManager.instance.player.GetComponent<Player>().setShield(50.0f);
        Destroy(PowerUpMenu);
        Time.timeScale = 1;
        GameManager.instance.is_level_menu_open = 0;
    }
}
