using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class GlobalLightModifier : MonoBehaviour
{
    [SerializeField]
    private Transform mainCamera;
    [SerializeField]
    private float worldYMax = -5;
    [SerializeField]
    private float worldYMin = -20;

    private Light2D intensityChange;
    Light globalLight;

    // Start is called before the first frame update
    void Start()
    {
        intensityChange = gameObject.GetComponent<Light2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 camPosition = mainCamera.position;
        float camYPosition = camPosition.y;
        //float between 0 and 1 below.
        // float worldYDelta = worldYMax - worldYMin;
        // float lightMapIntensitiy = camYPosition/worldYDelta;
        if(camYPosition>worldYMax){
            intensityChange.intensity = 1;    
        }
        else if(camYPosition>worldYMin){
            float lightMapIntensitiy = (camYPosition-worldYMin)/(worldYMax - worldYMin);
            intensityChange.intensity = lightMapIntensitiy;
        }
        //need to pass this value to the global light intensity.
        else{
            intensityChange.intensity = 0;
        }
    }
}