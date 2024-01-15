using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Log : MonoBehaviour
{
    [SerializeField] private float _health = 50.0f;
    // public GameObject damageText;

    // Update is called once per frame
    public void TakeDamage(float damage)
    {
        _health -= damage;
        // DamageIndicator indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
        // indicator.SetDamageText(damage);

        if (_health <= 0)
        {
            Destroy(this.gameObject);
            FindObjectOfType<AudioManager>().Play("Log Hit");

            Bounds bb = new Bounds(this.transform.position, new Vector3(1, 2, 1));
            AstarPath.active.UpdateGraphs(bb, 0);
        }
    }
    
}
