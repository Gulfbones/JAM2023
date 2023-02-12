using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class LightRayMovement : MonoBehaviour
{
    private Transform mainCamera;
    
    [SerializeField]
    private float worldXMin = -10f;
    [SerializeField]
    private float worldXMax = 10f;
    [SerializeField]
    private float lightXOffset = 0f;
    [SerializeField]
    private float lightMaxOffset = 5f;
    [SerializeField]
    private float baseIntensity = 0.5f;


    //rewrite this to take the base vector from the light potentially
    [SerializeField] 
    private float lightYOffset = 0f;
    private Vector3 lightVect;
    // Start is called before the first frame update
    void Start()
    {
                mainCamera = FindObjectOfType<Camera>().gameObject.transform;
                float sunIntensity = gameObject.GetComponent<Light2D>().intensity;
    }

    // Update is called once per frame
    void Update(){
        float sunIntensity = gameObject.GetComponent<Light2D>().intensity;
        Vector3 camPosition = mainCamera.position;
        float camXPosition = camPosition.x;
        if((camXPosition > 22 && camXPosition < 131) || camXPosition > 140){
            if(sunIntensity >0){
                sunIntensity-= 0.001f;
            }
            else{
                sunIntensity = 0f;
            }
        }
        else{
            if(sunIntensity < baseIntensity){
                sunIntensity+= 0.001f;
            }
            else{
                sunIntensity = baseIntensity;
            }
        }
        gameObject.GetComponent<Light2D>().intensity = sunIntensity;
        float worldDelta = worldXMax - worldXMin;
        float lightPositionFactor = camXPosition/worldDelta;
        float newLightPosition = camXPosition + lightMaxOffset*lightPositionFactor;
        lightVect = new Vector3(newLightPosition+lightXOffset, lightYOffset, 0f);
        gameObject.transform.position = lightVect;
    }
}
