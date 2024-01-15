using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandStartingPosition : MonoBehaviour
{
    public Transform topper;
    public float[] scaleY_Range;
    public float[] scaleXZ_Range;
    public float randPos_Range = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        RandomPosition();
        RandomRotation();
        RandomScale();
    }

    void RandomPosition()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-randPos_Range, randPos_Range), 0.5f, Random.Range(-randPos_Range, randPos_Range));
        topper.localPosition = randomPosition;
    }

    void RandomRotation() 
    {
        Vector3 randomRotation = new Vector3(0, Random.Range(-180f, 180f), 0);
        topper.Rotate(randomRotation);
    }

    void RandomScale() 
    {
        float xzScale = Random.Range(scaleXZ_Range[0], scaleXZ_Range[1]);
        Vector3 randomScale = new Vector3(xzScale, Random.Range(scaleY_Range[0], scaleY_Range[1]), xzScale);
        topper.localScale = randomScale;
    }
}
