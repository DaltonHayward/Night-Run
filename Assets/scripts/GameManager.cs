using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int is_level_menu_open = 0;

    [SerializeField] GameObject revivePanel;
    private GameObject revivePanelObj;

    [SerializeField] GameObject pauseMenu;
    private GameObject pauseMenuObj;

    [SerializeField] GameObject levelUpMenu;
    private GameObject levelUpMenuObj;
    private List<Image> fireWork;
    private Animator fireAnimator;

    public static GameManager instance = null;

    public GameObject player;
    public GameObject bullet;

    //Score variables
    public GameObject scoreTextPrefab;
    private GameObject scoreTextObj;
    private Text scoreText;
    public float score = 0.0f;

    private int zombie_kill = 0;
    private int mummy_kill = 0;
    private int slime_kill = 0;

    public float fireTime = 0;
    public float time;

    public float difficulty = 1.0f;

    public enum  monsterType{
        zombie,
        mummy,
        slime
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            FindObjectOfType<AudioManager>().Stop("Upgrade Shop");
            FindObjectOfType<AudioManager>().Play("Shadow Run");
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start() {
        zombie_kill = 0;
        slime_kill = 0;
        mummy_kill = 0;
        score = 0;
        scoreTextObj = Instantiate(scoreTextPrefab, FindObjectOfType<Canvas>().transform);
        scoreText = scoreTextObj.GetComponent<Text>();
    }

    void Update(){
        scoreText.text = Time.timeSinceLevelLoad.ToString("0.0");
        time = Time.timeSinceLevelLoad;
        score = time;

        if (Input.GetKeyDown(KeyCode.Escape) && is_level_menu_open == 0) openPause();

    }

    public void openPause(){

        if (Time.timeScale == 1){
            Time.timeScale = 0;
            pauseMenuObj = Instantiate(pauseMenu, FindObjectOfType<Canvas>().transform);
        }else{
            Destroy(revivePanelObj);
            Destroy(pauseMenuObj);
            Time.timeScale = 1;
        }
    }

    public void openRevivePanel(){
        if (revivePanel != null){
            revivePanelObj = Instantiate(revivePanel, FindObjectOfType<Canvas>().transform);
        }
    }

    public void openLevelUpMenu(){
        Time.timeScale = 0;
        is_level_menu_open = 1;
        levelUpMenuObj = Instantiate(levelUpMenu, FindObjectOfType<Canvas>().transform);
        fireWork = new List<Image>(levelUpMenuObj.GetComponentsInChildren<Image>());

        foreach (var img in fireWork)
        {
            if (img.tag == "firework"){
                fireAnimator = img.GetComponent<Animator>();
                fireAnimator.Play("firework_default", 0, 0);
            }
        }
        //animator = GetComponent<Animator>();
    }

    public void add_kill(monsterType enemy_Type){
        if (enemy_Type == monsterType.slime){
            slime_kill += 1;
        }else if (enemy_Type == monsterType.mummy){
            mummy_kill += 1;
        } else {
            zombie_kill += 1;
        }
    }

    public int get_zombies_kill(){
        return zombie_kill;
    }

    public int get_slimes_kill(){
        return  slime_kill;
    }

    public int get_mummy_kill(){
        return mummy_kill;
    }

    public Save getSavedData(){
        Save loadedData = new Save();
        string jsonData = PlayerPrefs.GetString("playeStats");
        //Convert to Class
        loadedData = JsonUtility.FromJson<Save>(jsonData);
        return loadedData;
    }

}
