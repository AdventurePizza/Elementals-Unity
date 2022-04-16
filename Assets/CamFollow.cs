using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public GameObject player;        
    public bool follow = true; 
    private Vector3 offset = Vector3.zero;



    public void SetPlayer(GameObject player){
        this.player = player;
        offset = transform.position - player.transform.position;
        offset = new Vector3(offset.x, 0, offset.z);
    }
    
    public void SetFollow(bool follow){
        this.follow = follow;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate () 
    {
        if(player && follow){
            Vector3 temp = player.transform.position + new Vector3(0.0f, 0.0f, -10f) + offset;
            transform.position = new Vector3(temp.x, 0.03f, temp.z);
        }
    }
}
