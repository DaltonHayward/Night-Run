using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquipped : MonoBehaviour
{
    public int selectedWeapon = 0;
    Save saveData;
    // Start is called before the first frame update
    void Start()
    {
        selectWeapon();
        this.transform.parent.GetComponent<Player>().setWeapon(selectedWeapon);

        saveData = GameManager.instance.getSavedData();
    }

    // Update is called once per frame
    void Update()
    {

        int previousSelectedWeapon = selectedWeapon;
        if (Input.GetKey(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        if (Input.GetKey(KeyCode.Alpha2) && saveData.has_sub_machine)
        {
            selectedWeapon = 1;
        }
        if (Input.GetKey(KeyCode.Alpha3) && saveData.has_shotgun)
        {
            selectedWeapon = 2;
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            selectWeapon();
        }
    }

    void selectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
        this.transform.parent.GetComponent<Player>().setWeapon(selectedWeapon);
    }
}
