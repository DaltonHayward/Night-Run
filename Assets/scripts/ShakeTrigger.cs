using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeTrigger : MonoBehaviour
{
    public ShakeEffect shakeEffect;
    

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(shakeEffect.Shake(.15f,.4f));
    }
}
