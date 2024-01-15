using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class Mummy: MonoBehaviour
{

    [SerializeField] GameObject barPrefab;
    GameObject healthBarObj;
    private Image healthBar;
    private Image healthBarFilled;

    [SerializeField] GameObject notchPrefab;
    private float numberOfNotches;
    private Image notch;
    GameObject notchObj;

    [SerializeField] private float health = 100.0f;
    [SerializeField] public float maxHealth = 100.0f;

    [SerializeField] private float damageToPlayer = 20.0f;
    //[SerializeField] private float damageRate = 0.2f;
    public Animator animator = null;
    public GameObject xpModel;
    private bool dead = false;
    private float attacking = 0;
    private Player attackTarget;

    [SerializeField] private float difficultyScaling = 0.01f;

    // public GameObject damageText;
    void Awake() 
    {
        GetComponent<AIDestinationSetter>().target = GameManager.instance.player.transform;
    }

    void Start(){
        maxHealth = maxHealth * GameManager.instance.difficulty;
        health = maxHealth;

        healthBarObj = Instantiate(barPrefab, FindObjectOfType<Canvas>().transform);
        healthBar = healthBarObj.GetComponent<Image>();
        setNotches(healthBarObj, healthBar.rectTransform.rect.width);

        healthBarFilled = new List<Image>(healthBar.GetComponentsInChildren<Image>()).Find(img => img != healthBar);
        healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0f, 0f, 1f)); //+ new Vector3(0, 0.5f, 1f)
    }
    
    // Update is called once per frame
    void Update()
    {
        //If not dead
        if (!dead)
        {
            // if attacking
            if (attacking > 0)
            {
                attacking -= Time.deltaTime;
                if (attacking < 0.8 && attackTarget != null)
                {
                    attackTarget.TakeDamage(damageToPlayer);
                    attackTarget = null;
                }

            }

            healthBar.transform.position = Camera.main.WorldToScreenPoint( transform.position + new Vector3(0f, 0f, 1f)); //+ new Vector3(5f, 0.5f, 1.5f)
        }
        
        else 
        {
            GetComponent<AIPath>().enabled = false;
        }
    }


    public void TakeDamage(float damage)
    {
        if (!dead)
        {
            CalculateHealth();
            health -= damage;
            healthBarFilled.fillAmount = health / maxHealth;
            // DamageIndicator indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
            // indicator.SetDamageText(damage);

            if (health <= 0)
            {
                DropXP();
                GameManager.monsterType mummy = GameManager.monsterType.mummy;
                GameManager.instance.add_kill(mummy);
                GameManager.instance.difficulty += difficultyScaling;
                Destroy(this.gameObject);
                Destroy(healthBar.gameObject);
                dead = true;
            }
        }
    }

    private void setNotches(GameObject healthBar, float healthbarWidth){

        numberOfNotches = Mathf.Floor(health/10);

        float barMidPoint = healthbarWidth/2;
        float barStartPoint = -barMidPoint;
        float notchesInterval = healthbarWidth/numberOfNotches;
        
        for(int i = 0; i< numberOfNotches-1; i++){
            notchObj = Instantiate(notchPrefab, FindObjectOfType<Canvas>().transform);
            notchObj.transform.SetParent(healthBar.transform);
            float notchXPosition = barStartPoint + notchesInterval;
            barStartPoint += notchesInterval;
            notchObj.transform.localPosition = new Vector3(notchXPosition, 0, 0);
        }

    }

    //Drop XP object which will be then reflected on XPBar in future
    void DropXP()
    {
        Vector3 position = transform.position;
        GameObject xp = Instantiate(xpModel, position + new Vector3(0, 0.2f, 0), Quaternion.identity);
    }

    void OnTriggerEnter(Collider other)
    {
        // If we found a player, start attacking
        if (other.transform.tag == "Player" && other.GetType() == typeof(CapsuleCollider))
        {
            attackTarget = other.transform.GetComponent<Player>();
            attacking = 1;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // If the player leaves our collider area, no longer have attackTarget
        if (other.transform.tag == "Player")
        {
            attackTarget = null;
        }
    }

    float CalculateHealth()
    {
        return health / maxHealth;
    }


}