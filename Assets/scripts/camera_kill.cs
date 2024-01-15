using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_kill : MonoBehaviour
{
    private bool has_revive = false;

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)    {
        // If we found a player, start attacking
        Debug.Log("OOOOOOOOOOOOOOOOOOOOOOOOPS kill " +other.transform.tag);
        has_revive = GameManager.instance.player.GetComponent<Player>().has_revive();
        if (other.transform.tag == "Player" && other.GetType() == typeof(CapsuleCollider) && !has_revive)
        {
            GameManager.instance.player.GetComponent<Player>().die();
        }
    }
}
