using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;

public class shopManager : MonoBehaviour
{
    private bool max_health_flag = false;
    private Save saveData;

    // public GameObject coinsTextPrefab;
    // private GameObject coinsTextObj;
    private Text coinsText;

    private float shop_price_hp = 10.0f;
    private float shop_price_extra = 10.0f;
    private float shop_price_shield = 10.0f;
    private float shop_price_shotgun = 10.0f;
    private float shop_price_AK = 10.0f;
    private float shop_price_other = 10.0f;

    private Button button_hp;
    private Button button_extra;
    private Button button_shield;
    private Button button_shotgun;
    private Button button_ak;

    private bool hp_flag = false;
    
    // Start is called before the first frame update
    void Start()
    {
        string jsonData = PlayerPrefs.GetString("playeStats");
        //Convert to Class
        saveData = JsonUtility.FromJson<Save>(jsonData);

        saveData.max_health = saveData.default_maxHealth;
        saveData.extra_life = false;
        saveData.has_shotgun = false;
        saveData.has_sub_machine = false;
        saveData.shield_value = 0;
        saveData.kills_total = 0;
        saveData.zombie_kills = 0;
        saveData.slime_kills = 0;
        saveData.mummy_kills = 0;

        List<Text> price = new List<Text>(GameObject.Find("Canvas").GetComponentsInChildren<Text>());

        List<Button> buttons = new List<Button>(GameObject.Find("Canvas").GetComponentsInChildren<Button>());

        foreach(var bt in buttons){
            switch (bt.tag){
                case "shop_hp":
                    Debug.Log("Button 1");
                    button_hp = bt;
                break;
                case "shop_extra":
                    Debug.Log("Button 2");
                    button_extra = bt;
                break;
                case "shop_shield":
                    Debug.Log("Button 3");
                    button_shield = bt;
                break;
                case "shop_shotgun":
                    Debug.Log("Button 4");
                    button_shotgun = bt;
                break;
                case "shop_ak":
                    Debug.Log("Button 5");
                    button_ak = bt;
                break;
                default:
                break;

            }
        }


        for (int i = 0; i < price.Count; i++) {
            if (price[i].tag == "shop_hp"){
                price[i].text = "$"+shop_price_hp.ToString("0.0");

            }else if (price[i].tag == "shop_extra"){
                price[i].text = "$"+shop_price_extra.ToString("0.0");

            }else if (price[i].tag == "shop_shield"){
                price[i].text = "$"+shop_price_shield.ToString("0.0");

            }else if (price[i].tag == "shop_shotgun"){
                price[i].text = "$"+shop_price_shotgun.ToString("0.0");

            }else if (price[i].tag == "shop_ak"){
                price[i].text = "$"+shop_price_AK.ToString("0.0");

            }

            if (price[i].tag == "shop_coins"){
                coinsText = price[i];
            }
        }

        check_buttons_status();

        coinsText.text = "$" + saveData.money;

        jsonData = JsonUtility.ToJson(saveData);
        //Save Json string
        PlayerPrefs.SetString("playeStats", jsonData);
        PlayerPrefs.Save();

    }


    public void buy_upgrades(int buttom_id){

        string jsonData = PlayerPrefs.GetString("playeStats");
        //Convert to Class
        saveData = JsonUtility.FromJson<Save>(jsonData);

        float total_coins = (float) Convert.ToDouble(coinsText.text.Substring(1)); 

        Debug.Log("TOTAL COINS: " + total_coins);
        
        switch (buttom_id){
            case 1:
                Debug.Log("Pressed on 1");
                if (total_coins >=  shop_price_hp && !hp_flag){
                    saveData.max_health += 100;
                    total_coins =   total_coins - shop_price_hp;
                    coinsText.text = "$" + total_coins.ToString("0.0");
                    hp_flag = true;
                }

            break;
            case 2:
                if (total_coins >= shop_price_extra && !saveData.extra_life){
                    total_coins -= shop_price_hp;
                    saveData.extra_life = true;
                    coinsText.text = "$" + total_coins.ToString("0.0");
                }

            break;
            case 3:
                bool shield_val_check = true;

                if (saveData.shield_value >= saveData.max_health){
                    shield_val_check = false;
                }

                if (total_coins >= shop_price_shield && shield_val_check){
                    total_coins -= shop_price_shield;
                    saveData.shield_value += 100;
                    coinsText.text = "$" + total_coins.ToString("0.0");
                }


            break;
            case 4:
                if (total_coins >= shop_price_shotgun && !saveData.has_shotgun){
                    total_coins -= shop_price_shotgun;
                    saveData.has_shotgun = true;
                    coinsText.text = "$" + total_coins.ToString("0.0");
                }

            break;
            case 5:
                if (total_coins >= shop_price_AK && !saveData.has_sub_machine){
                    total_coins -= shop_price_AK;
                    saveData.has_sub_machine = true;
                    coinsText.text = "$" + total_coins.ToString("0.0");
                }

            break;
            case 6:

            default:
                // saveData.max_health = saveData.default_maxHealth;
                // saveData.extra_life = false;
            break;

        }

        check_buttons_status();
        jsonData = JsonUtility.ToJson(saveData);
        //Save Json string
        PlayerPrefs.SetString("playeStats", jsonData);
        PlayerPrefs.Save();
    }

    private void check_buttons_status(){
        float total_coins = (float) Convert.ToDouble(coinsText.text.Substring(1));

        if (shop_price_hp > total_coins || hp_flag){
            button_hp.interactable = false;
        }else{
            button_hp.interactable = true;
        }

        if (shop_price_extra > total_coins || saveData.extra_life){
            button_extra.interactable = false;
        }else{
            button_extra.interactable = true;
        }

        bool shield_val_check = true;

        if (saveData.shield_value >= saveData.max_health){
            shield_val_check = false;
        }

        if (shop_price_shield > total_coins){
            button_shield.interactable = false;
        }else{
            button_shield.interactable = true;
        }

        if (shop_price_shotgun > total_coins || saveData.has_shotgun){
            button_shotgun.interactable = false;
        }else{
            button_shotgun.interactable = true;
        }

        if (shop_price_AK > total_coins || saveData.has_sub_machine){
            button_ak.interactable = false;
        }else{
            button_ak.interactable = true;
        }
    }
}
