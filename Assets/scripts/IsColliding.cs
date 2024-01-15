using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsColliding : MonoBehaviour
{
    public bool isColliding = false;


    void FixedUpdate() {
        isColliding = false;
    }

    void OnTriggerStay(Collider other) {
        if (other.transform.tag == "Obsticle" || other.transform.tag == "Log Obsticle"){
            isColliding = true;
        }
    }

    public bool getCollision() {
        return isColliding;
    }
}
