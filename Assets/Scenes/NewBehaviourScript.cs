using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject caiman;
    public bool based = false;
    
    // Start is called before the first frame update
    void Start()
    {

 	caiman = GameObject.Find("caiman");

    }

    void redpill()
    {
    	based = true;
    }
	
    // Update is called once per frame
    void Update()
    {
    	if(!based){
        	caiman.transform.Rotate(0, 100 * Time.deltaTime, 0);
        }
    }
}
