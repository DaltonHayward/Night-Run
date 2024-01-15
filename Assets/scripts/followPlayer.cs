using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    [SerializeField] public float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    public void reset_on_survive(Vector3 reset_position){

        transform.position = new Vector3(transform.position.x , transform.position.y, reset_position.z);
    }
}
