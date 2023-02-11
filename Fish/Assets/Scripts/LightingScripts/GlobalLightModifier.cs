using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLightModifier : MonoBehaviour
{
    [SerializeField]
    private Transform mainCamera;
    [SerializeField]
    private float worldYMax = -5;
    [SerializeField]
    private float worldYMin = -20;

    Light globalLight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 camPosition = mainCamera.position;
        float camYPosition = camPosition.y;
        float worldYDelta = worldYMax - worldYMin;
        //float between 0 and 1 below.
        float lightMapIntensitiy = camYPosition/worldYDelta;
        //need to pass this value to the global light intensity.
        

    }
}