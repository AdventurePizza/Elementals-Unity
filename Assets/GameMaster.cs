using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class GameMaster : MonoBehaviour 
 {
    public static GameMaster GM;
    public GameObject hero;
     
    
    void Awake()
    {
        Debug.Log("awake GM");
        if(GM != null)
            GameObject.Destroy(GM);
        else
            GM = this;
        
        //DontDestroyOnLoad(this);

        if(hero){
            Instantiate(hero, new Vector3(0, -0.57f, 0), Quaternion.identity);
        }
    }

    public void setHero(GameObject hero){
        this.hero = hero;
    }

 }
