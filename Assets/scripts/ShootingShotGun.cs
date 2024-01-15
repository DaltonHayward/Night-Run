using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingShotGun : MonoBehaviour
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


    public void Shoot(){
        
        // transform.rotation = currentRotation;
        if(GameManager.instance.time >= GameManager.instance.fireTime) {
            GameManager.instance.fireTime += fireRate;

            Instantiate(bullet, tipOfGun.transform.position, transform.rotation * Quaternion.Euler(0, -20,0));
            Instantiate(bullet, tipOfGun.transform.position, transform.rotation * Quaternion.Euler(0, -10,0));
            Instantiate(bullet, tipOfGun.transform.position, transform.rotation * Quaternion.Euler(0, 0,0));
            Instantiate(bullet, tipOfGun.transform.position, transform.rotation * Quaternion.Euler(0, 10,0));
            Instantiate(bullet, tipOfGun.transform.position, transform.rotation * Quaternion.Euler(0, 20,0));
            muzzleFlash.Play();

            StartCoroutine(Camera.main.GetComponent<ShakeEffect>().Shake(.01f,.05f));
            FindObjectOfType<AudioManager>().Play("Shotgun Fire");
            
        }
    }
}
