using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField] private float maxHealth;
    [SerializeField] private float xp_total;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject XPBar;
    [SerializeField] private GameObject levelBox;

    private float health;
    private float shield;
    public GameObject model;
    private Animator animator;
    private MeshRenderer playerRenderer;
    private bool isHopping;

    private GameObject healthBarObj;
    private Image healthBarImg;
    private Image healthBarFilled;
    private Image shieldBarFilled;
    private Text healthBarText;

    private GameObject xpBarObj;
    private Image xpImage;
    private Image xpImagefilled;
    private float xp;

    private GameObject levelBoxObj;
    private Text levelText;
    private int playerLevel;
    private float currentTime;

    private bool offCam = true;

    private bool atBoundaries;

    public GameObject DirectionalCollider;
    private bool leftColliding;
    private bool rightColliding;
    private bool forwardColliding;
    private bool backwardColliding;
    public int weapon = 0;

    Save saveData ;

    public bool reviving = false;

    // Start is called before the first frame update
    void Start()
    {

        
        saveData = GameManager.instance.getSavedData();

        maxHealth = saveData.max_health;
        shield = saveData.shield_value;

        
        health = maxHealth;
        animator = GetComponent<Animator>();
        playerRenderer = model.GetComponent<MeshRenderer>();

        // --------- INITIALIZE HEALTH BAR --------- //
        healthBarObj = Instantiate(healthBar, FindObjectOfType<Canvas>().transform);
        healthBarImg = healthBarObj.GetComponent<Image>();
        healthBarFilled = new List<Image>(healthBarImg.GetComponentsInChildren<Image>()).Find(img => img.tag == "player_filled_bar");
        shieldBarFilled = new List<Image>(healthBarImg.GetComponentsInChildren<Image>()).Find(img => img.tag == "player_shield_bar");

        healthBarText = healthBarObj.GetComponentsInChildren<Text>()[0];

        if (shield > 0){
            shieldBarFilled.fillAmount = shield/maxHealth;
            healthBarFilled.fillAmount = 1;
            healthBarText.text = shield.ToString() + "/" + maxHealth.ToString();
        }else{
            healthBarText.text = maxHealth.ToString() + "/" + maxHealth.ToString();
            healthBarFilled.fillAmount = 1;
        }
        

        // --------- XP HEALTH BAR --------- //
        xpBarObj = Instantiate(XPBar, FindObjectOfType<Canvas>().transform);
        xpImage = xpBarObj.GetComponent<Image>();
        xpImagefilled = new List<Image>(xpImage.GetComponentsInChildren<Image>()).Find(img => img != xpImage);

        // --------- PLAYER LEVEL --------- //
        playerLevel = 0;
        levelBoxObj = Instantiate(levelBox, FindObjectOfType<Canvas>().transform);
        levelText = levelBoxObj.GetComponentsInChildren<Text>()[0];
        levelText.text = "Level: " + playerLevel.ToString();

        CheckCollision();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        offCamera();

        LookAtMouse();

        CheckCollision();
    }


    private void MovePlayer()
    {
        if (Input.GetKey(KeyCode.W) && !isHopping && atBoundaries){
            MoveCharacter(new Vector3(0,0,0f));
        }
        // with collision
        else if (Input.GetKey(KeyCode.W) && !isHopping && !atBoundaries)
        {
            if (!forwardColliding)
            {
                MoveCharacter(new Vector3(0, 0, 1f));
            }
            else
            {
                animator.SetTrigger("hop");
                isHopping = true;
            }
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (Input.GetKey(KeyCode.A) && !isHopping)
        {
            if (!leftColliding)
            {
                MoveCharacter(new Vector3(-1f, 0, 0));
            }
            else
            {
                animator.SetTrigger("hop");
                isHopping = true;
            }
            transform.eulerAngles = new Vector3(0, -90, 0);

        }
        else if (Input.GetKey(KeyCode.D) && !isHopping)
        {
            if (!rightColliding)
            {
                MoveCharacter(new Vector3(1f, 0, 0));
            }
            else
            {
                animator.SetTrigger("hop");
                isHopping = true;
            }
            transform.eulerAngles = new Vector3(0, 90, 0);

        }
        else if (Input.GetKey(KeyCode.S) && !isHopping)
        {
            if (!backwardColliding)
            {
                MoveCharacter(new Vector3(0, 0, -1f));
            }
            else
            {
                animator.SetTrigger("hop");
                isHopping = true;
            }
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

    }


    public void FinishHop()
    {
        isHopping = false;
    }

    private void MoveCharacter(Vector3 difference)
    {
        animator.SetTrigger("hop");
        isHopping = true;
        transform.position = (transform.position + difference);
        FindObjectOfType<AudioManager>().Play("Hopping");
    }

    private void offCamera()
    {
        float offset = 5.0f;
        float camerabounds = 21.5f;

        if (Camera.main.transform.position.z >= (transform.position.z + offset) && offCam)
        {
            if (saveData.extra_life){
                revive();
            }else{
                die();
            }
            
        }
        // if the player is at the top boundary of the
        if ((Camera.main.transform.position.z + camerabounds) <= (transform.position.z + offset))
        {
            atBoundaries = true;
        }
        else{
            atBoundaries = false;
        }

    }

    public void TakeDamage(float damage)
    {

        if (shield > 0){

            shield -= damage;

            if (shield <= 0){

                Debug.Log("SHILD WILL BE DESTROYED");
                Debug.Log(health);
                Debug.Log(shield);
                health -= (-shield);
                Debug.Log("SHILD WILL BE DESTROYED END");

                shieldBarFilled.fillAmount = 0;
                healthBarFilled.fillAmount = health / maxHealth;
                healthBarText.text = health.ToString() + "/" + maxHealth.ToString();
            }else{
                healthBarText.text = shield.ToString() + "/" + maxHealth.ToString();
                shieldBarFilled.fillAmount = shield / maxHealth;
            }
        }else{
            if (health > 0)
            {
                //StartCoroutine(Camera.main.GetComponent<ShakeEffect>().Shake(.1f,.05f));
                health -= damage;
                FindObjectOfType<AudioManager>().Play("Player Hurt");
                healthBarFilled.fillAmount = health / maxHealth;
                healthBarText.text = health.ToString() + "/" + maxHealth.ToString();
            }
        }

        if (health <= 0)
        {
            health = 0;

            if (saveData.extra_life){
                revive();
            }else{
                die();
            }
            
        }
    }

    public void addXpPoints(float xp)
    {
        this.xp += xp;
        xpImagefilled.fillAmount += xp / xp_total;

        if (xpImagefilled.fillAmount == 1)
        {
            GameManager.instance.is_level_menu_open = 1;
            GameManager.instance.openLevelUpMenu();
            xpImagefilled.fillAmount = 0;
            playerLevel += 1;
            levelText.text = "Level: " + playerLevel.ToString();

            xp_total += 10f;
        }

    }


    // 360 rotation of the player towards the mouse position
    public void LookAtMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // create a ray from the mouse position of screen to a world point
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray, Mathf.Infinity); // cast the ray through all objects
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.tag == "Ground")
            { // check that the ray collides with the ground and only the ground
                Debug.DrawRay(hits[i].transform.position, hits[i].transform.forward, Color.green);
                transform.LookAt(hits[i].point); // Look at the point
                transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0)); // Clamp the x and z rotation
            }
        }
    }

    public void refillHealth()
    {
        maxHealth += 25;
        health = maxHealth;
        healthBarFilled.fillAmount = 1;
        healthBarText.text = maxHealth.ToString() + "/" + maxHealth.ToString();
    }

    private void CheckCollision()
    {
        leftColliding = DirectionalCollider.transform.GetChild(0).GetComponent<IsColliding>().getCollision();
        rightColliding = DirectionalCollider.transform.GetChild(1).GetComponent<IsColliding>().getCollision();
        forwardColliding = DirectionalCollider.transform.GetChild(2).GetComponent<IsColliding>().getCollision();
        backwardColliding = DirectionalCollider.transform.GetChild(3).GetComponent<IsColliding>().getCollision();
    }

    public void save()
    {
        string jsonData = JsonUtility.ToJson(saveData);
        //Save Json string
        PlayerPrefs.SetString("playeStats", jsonData);
        PlayerPrefs.Save();
    }


    public void die()
    {

        saveData.score = GameManager.instance.score;

        saveData.zombie_kills = GameManager.instance.get_zombies_kill();
        saveData.mummy_kills = GameManager.instance.get_mummy_kill();
        saveData.slime_kills = GameManager.instance.get_slimes_kill();

        saveData.kills_total = saveData.slime_kills + saveData.mummy_kills + saveData.zombie_kills;

        saveData.has_shotgun = false;
        saveData.has_sub_machine = false;
        saveData.max_health = saveData.default_maxHealth;
        saveData.extra_life = false;
        saveData.shield_value = 0;

        string jsonData = JsonUtility.ToJson(saveData);
        //Save Json string
        PlayerPrefs.SetString("playeStats", jsonData);
        PlayerPrefs.Save();

        FindObjectOfType<AudioManager>().Play("Player Death");
        Destroy(this.gameObject);
        SceneManager.LoadScene("DeathMenuScene");

    }

    public void setWeapon(int w)
    {
        weapon = w;
        for (int i = 0; i < 5; i++)
        {
            Camera.main.transform.GetChild(i).GetComponent<Spawner>().selectedW = w;
        }
        
    }

    public int getWeapon()
    {
        return weapon;
    }

    public void setShield(float shield_val){

        if ((shield + shield_val) > maxHealth){
            shield = maxHealth;
        }else{
            shield += shield_val;
            healthBarText.text = shield.ToString() + "/" + maxHealth.ToString();
            shieldBarFilled.fillAmount = shield / maxHealth;
        }
    }

    public bool has_revive(){
        return saveData.extra_life;
    }

    public void revive(){

        Debug.Log("ON REVIVE");
        GameObject.Find("followPlayer").GetComponent<followPlayer>().reset_on_survive(this.transform.position);
        
        GameManager.instance.openRevivePanel();

        healthBarFilled.fillAmount = 1;
        shieldBarFilled.fillAmount = 0;
        health = saveData.max_health;
        healthBarText.text = health.ToString() + "/" + maxHealth.ToString();
        Time.timeScale = 0;
        StartCoroutine(revivePlayer());       
    }


    public IEnumerator revivePlayer()
    {
        // Destroy(this.gameObject);
        yield return new WaitForSeconds(3);
        saveData.extra_life = false;
    }


}
