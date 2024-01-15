using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingMG : MonoBehaviour
{
    public GameObject bullet;
    public float fireRate = 0.1f;
    public float fireTime;
    private float time;
    public GameObject tipOfGun;
    public ParticleSystem muzzleFlash;


    // Update is called once per frame
    void Update()
    { 
        Shoot();
    }

    private void Shoot(){
        if(GameManager.instance.time >= GameManager.instance.fireTime) {
            GameManager.instance.fireTime += fireRate;

            Instantiate(bullet, tipOfGun.transform.position , transform.rotation);
            muzzleFlash.Play();

            StartCoroutine(Camera.main.GetComponent<ShakeEffect>().Shake(.09f,.008f));
            FindObjectOfType<AudioManager>().Play("Gun Fire");
            
        }
    }
}
