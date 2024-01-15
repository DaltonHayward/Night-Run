using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public int kills_total = 0;
    public int zombie_kills = 0;
    public int slime_kills = 0;
    public int mummy_kills = 0;

    public float score = 0.0f;
    public float money = 0.0f;

    public bool extra_life = false;

    public float default_maxHealth = 100.0f;
    public float max_health = 100.0f;

    public float shield_value = 0.0f;


    public int record_kills = 0;
    public int record_zombie_kills = 0;
    public int record_slime_kills = 0;
    public int record_mummy_kills = 0;

    public bool has_shotgun = false;
    public bool has_sub_machine = false;
}
