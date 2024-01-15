using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP : MonoBehaviour
{
    [SerializeField] public float xpAmount = 1.0f;
    [SerializeField] float xpMovementSpeed = 5.0f;
    private bool triggerMovement = false;

    public void Update()
    {
        Movement(triggerMovement);

        Vector3 xpPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (xpPosition.z < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.transform.tag == "Player"){
    
            if(other.GetType() == typeof(SphereCollider)){
                triggerMovement = true;
            }else{
                GameManager.instance.player.GetComponent<Player>().addXpPoints(xpAmount);
                Destroy(this.gameObject);
            }
        }
    }

    public void Movement(bool toggle){

        if (GameManager.instance.player && toggle){
            transform.LookAt(GameManager.instance.player.transform.position); //Look at the player
            transform.position += transform.forward * xpMovementSpeed * Time.deltaTime;
        }
    }
}
