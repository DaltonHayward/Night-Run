using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class deathLoading : MonoBehaviour
{

    Save loadedData;
    //Score variables
    public GameObject zombiesTextPrefab;
    private GameObject zombiesTextObj;
    private Text zombiesText;

    public GameObject mummyTextPrefab;
    private GameObject mummyTextObj;
    private Text mummyText;

    public GameObject slimeTextPrefab;
    private GameObject slimeTextObj;
    private Text slimeText;

    public GameObject killsTextPrefab;
    private GameObject killsTextObj;
    private Text killsText;

    public GameObject scoreTextPrefab;
    private GameObject scoreTextObj;
    private Text scoreText;

    public GameObject moneyTextPrefab;
    private GameObject moneyTextObj;
    private Text moneyText;


    // //Score variables
    // public GameObject scoreTextPrefab;
    // private GameObject scoreTextObj;
    // private Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        string jsonData = PlayerPrefs.GetString("playeStats");
        //Convert to Class
        loadedData = JsonUtility.FromJson<Save>(jsonData);

        // ---------------------- Add total zombies killed ----------------------------------//
        zombiesTextObj = Instantiate(zombiesTextPrefab, FindObjectOfType<Canvas>().transform);
        zombiesText = zombiesTextObj.GetComponent<Text>();
        zombiesText.text += " " + loadedData.zombie_kills.ToString();

        // ---------------------- Add total mummies killed ----------------------------------//
        mummyTextObj = Instantiate(mummyTextPrefab, FindObjectOfType<Canvas>().transform);
        mummyText = mummyTextObj.GetComponent<Text>();
        mummyText.text += " " + loadedData.mummy_kills.ToString();

        // ---------------------- Add total slimes killed ----------------------------------//
        slimeTextObj = Instantiate(slimeTextPrefab, FindObjectOfType<Canvas>().transform);
        slimeText = slimeTextObj.GetComponent<Text>();
        slimeText.text += " " + loadedData.slime_kills.ToString();

        // ---------------------- Add total killed ----------------------------------//
        killsTextObj = Instantiate(killsTextPrefab, FindObjectOfType<Canvas>().transform);
        killsText = killsTextObj.GetComponent<Text>();
        killsText.text += " " + loadedData.kills_total.ToString();

        int total_killed_to_coins = 2 * loadedData.kills_total;

        // ---------------------- Add time survived ----------------------------------//
        scoreTextObj = Instantiate(scoreTextPrefab, FindObjectOfType<Canvas>().transform);
        scoreText = scoreTextObj.GetComponent<Text>();
        scoreText.text += " " + loadedData.score.ToString("0.0");

        // ---------------------- Add total zombies killed ----------------------------------//
        moneyTextPrefab = Instantiate(moneyTextPrefab, FindObjectOfType<Canvas>().transform);
        moneyText = moneyTextPrefab.GetComponent<Text>();
        moneyText.text += " " + total_killed_to_coins.ToString("0.0");

        loadedData.money = total_killed_to_coins;

        jsonData = JsonUtility.ToJson(loadedData);

        PlayerPrefs.SetString("playeStats", jsonData);
        PlayerPrefs.Save();

        Save saveData = new Save();

    }
}
