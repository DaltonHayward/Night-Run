using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform player_target;
    [SerializeField] public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = player_target.position + offset;
    }
}