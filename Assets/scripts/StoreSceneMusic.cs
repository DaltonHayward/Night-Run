using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreSceneMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Stop("Shadow Run");
        FindObjectOfType<AudioManager>().Stop("Main Menu");
        FindObjectOfType<AudioManager>().Play("Upgrade Shop");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
