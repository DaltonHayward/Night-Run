using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    public void PlayButtonSound(){
        FindObjectOfType<AudioManager>().Play("Button Sound");
    }
}
