using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPistol : MonoBehaviour
{

    public GameObject bullet;
    public float fireRate = 10.0f;
    public float fireTime = 0.0f;
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

            StartCoroutine(Camera.main.GetComponent<ShakeEffect>().Shake(.008f,.03f));
            FindObjectOfType<AudioManager>().Play("Gun Fire");
            
        }
    }
}
