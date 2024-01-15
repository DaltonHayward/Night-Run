using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float lifeTime = 2.0f;
    [SerializeField] float initialDamage = 50.0f;
    private float damage = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        damage = initialDamage;
        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        MoveProjectile();
    }

    public void increaseDamage(float dmg_increase){
        
        damage += dmg_increase;
        Debug.Log(damage);
    }

    private void MoveProjectile() {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

     void OnTriggerEnter(Collider other){
        //Debug.Log("BUT THE DMG IS " + damage);
        if(other.transform.tag == "Enemy" && other.GetComponent<Enemy>().dead != true){
            other.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
        if(other.transform.tag == "Slime")
        {
            other.GetComponent<Slime>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
        if (other.transform.tag == "Mummy")
        {
            other.GetComponent<Mummy>().TakeDamage(damage);
            Destroy(this.gameObject);
        }

        if (other.transform.tag == "Obsticle")
        {
            Destroy(this.gameObject);
        }

        if (other.transform.tag == "Log Obsticle")
        {
            other.GetComponent<Log>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
     }
}
