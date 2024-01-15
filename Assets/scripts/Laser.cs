using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lr;
    // Start is called before the first frame update
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.alignment = LineAlignment.View;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, transform.forward, out hit)) {
            if (hit.collider.tag == "Obsticle" || hit.collider.tag == "Enemy" || hit.collider.tag == "Log Obsticle") {
                lr.SetPosition(1, new Vector3(0, 0, hit.distance));
            }
        }
        else {
            lr.SetPosition(1, new Vector3(0, 0, 5000));
        }
        
    }
}
