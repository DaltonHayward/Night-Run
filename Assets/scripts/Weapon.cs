using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private GameObject tipOfGun;

    private void Awake() 
    {
        tipOfGun = GameObject.FindGameObjectsWithTag("Laser")[0];
    }

    // Update is called once per frame
    void Update()
    {
        ShootLaser();
    }

    void ShootLaser() {

        RaycastHit hit;

        if (Physics.Raycast(tipOfGun.transform.position, tipOfGun.transform.forward, out hit)){
            if (hit.collider.tag == "Enemy") {
                print ("Hit : "+hit.collider.gameObject.name);
            }
        }
    }
}
