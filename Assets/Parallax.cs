using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos;
    public GameObject cam;
    public float parallexEffect;
    void Start()
    {
        startpos = transform.position.x;
        if(GetComponent<SpriteRenderer>()){
            length = GetComponent<SpriteRenderer>().bounds.size.x;
        }else{
            length = 16.128f;
        }
        Debug.Log(length);
        //length = 16.128f;
    }
    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallexEffect));
        float dist = (cam.transform.position.x * parallexEffect);
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
        if (temp > startpos + length) 
            startpos += length;
        else if (temp < startpos - length) 
            startpos -= length;
    }
}
