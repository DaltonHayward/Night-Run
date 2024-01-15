using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGrid : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // if (other.transform.tag == "Enemy") 
        // {
        //     other.GetComponent<Enemy>().TakeDamage(99999);
        // }
        // else if (other.transform.tag == "Mummy")
        // {
        //     other.GetComponent<Mummy>().TakeDamage(99999);
        // }
        // else if (other.transform.tag == "Slime")
        // {
        //     other.GetComponent<Slime>().TakeDamage(99999);

        // }
        // else
        {
            Destroy(other.gameObject);
        }
        
    }
}
