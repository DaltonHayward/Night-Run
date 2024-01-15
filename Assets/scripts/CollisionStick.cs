using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionStick : MonoBehaviour
{
    [SerializeField] private Transform _poisitonToFollow;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = _poisitonToFollow.position;
    }
}
