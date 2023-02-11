using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadlampAttached : MonoBehaviour
{
    [SerializeField]
    private bool isOn = true;
    [SerializeField]
    private Transform Owner;
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        if(isOn){
            gameObject.transform.position = Owner.position;    
        }
    }
}
