using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRayMovement : MonoBehaviour
{
    [SerializeField]
    private Transform mainCamera;
    [SerializeField]
    private float worldXMin = -10f;
    [SerializeField]
    private float worldXMax = 10f;
    [SerializeField]
    private float lightXOffset = 0f;
    [SerializeField]
    private float lightMaxOffset = 5f;

    //rewrite this to take the base vector from the light potentially
    [SerializeField] 
    private float lightYOffset = 0f;
    private Vector3 lightVect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 camPosition = mainCamera.position;
        float camXPosition = camPosition.x;
        float worldDelta = worldXMax - worldXMin;
        float lightPositionFactor = camXPosition/worldDelta;
        float newLightPosition = camXPosition + lightMaxOffset*lightPositionFactor;
        lightVect = new Vector3(newLightPosition+lightXOffset, lightYOffset, 0f);
        gameObject.transform.position = lightVect;
    }
}
