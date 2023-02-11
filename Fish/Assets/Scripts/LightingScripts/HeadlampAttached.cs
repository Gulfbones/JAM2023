using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class HeadlampAttached : MonoBehaviour
{
    [SerializeField]
    private bool isOn = true;
    [SerializeField]
    private Transform Owner;
    // Start is called before the first frame update
    private Light2D localLight;
    private Light2D globalLight;
    private float globalIntensity;
    void Start(){
        
        localLight = gameObject.GetComponent<Light2D>();
        // globalLight = FindObjectOfType<GlobalLightModifier>().gameObject.GetComponent<Light2D>();
        // globalIntensity = globalLight.intensity;
    }

    // Update is called once per frame
    void Update(){
        if(isOn){
            globalLight = FindObjectOfType<GlobalLightModifier>().gameObject.GetComponent<Light2D>();
            globalIntensity = globalLight.intensity;
            gameObject.transform.position = Owner.position; 
            localLight.intensity = 1 - globalIntensity;
            Debug.Log(globalIntensity);
            Debug.Log(localLight.intensity);
        }
    }
}
