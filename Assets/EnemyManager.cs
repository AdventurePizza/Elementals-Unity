using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody;
    public Animator anim;
    private BoxCollider2D boxCollider2d;
    private GameObject player;
    public GameObject FightManager;
    private string currentState = "idle"; 
    //private bool inRange = false;
    public int health  = 3000;
    public bool dead = false;
	public HealthBar healthBar;
    public GameObject hitInfo;
    public GameObject Camera;

    public AudioSource audioSource;
    public AudioClip takeHitSFX;
    public AudioClip attackSFX;

    // Start is called before the first frame update
    void Awake()
    {
        player = (GameObject.FindGameObjectsWithTag("Player"))[0];
        FightManager = (GameObject.FindGameObjectsWithTag("FightManager"))[0];
        m_Rigidbody = GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        healthBar.SetMaxHealth(health);
        Camera = player.GetComponent<CharacterManager>().Camera;
    }
    
    public void playAttack(){
        audioSource.PlayOneShot(attackSFX, 1);
    }

    public void takeHit(int damage, int knockback){
        health = health - damage;
        GameObject temp = Instantiate(hitInfo, gameObject.transform.position, Quaternion.identity);
        temp.GetComponent<HitInfo>().setText("-" + damage); 
        //Camera.GetComponent<CameraShake>().Shake(0.1f, 0.2f); 
        audioSource.PlayOneShot(takeHitSFX, 1);
        healthBar.SetHealth(health);
        if(health <= 0){
            if(!dead){
                anim.SetTrigger("death");
                dead = true;
                FightManager.GetComponent<FightManager>().setEnemyDead(); 
                player.GetComponent<CharacterManager>().nextEnemy();
                boxCollider2d.enabled = false;
                m_Rigidbody.bodyType = RigidbodyType2D.Static;
            }

        }
        else{
            anim.SetTrigger("takeHit");
        }
        //m_Rigidbody.AddForce(transform.right * knockback * -5);
    }

    public void miss(){
        GameObject temp = Instantiate(hitInfo, gameObject.transform.position, Quaternion.identity);
        temp.GetComponent<HitInfo>().setText("*miss*"); 
    }

    private void attack(){
        switch(currentState) 
        {
            case "attack1":
                Debug.Log("-70 (player)");
                player.GetComponent<CharacterManager>().takeHit(37, 100);
                break;
            case "attack2":
                Debug.Log("-170 (player)");
                player.GetComponent<CharacterManager>().takeHit(57, 100);
                break;
            case "attack":
                Debug.Log("-100 (player)");
                player.GetComponent<CharacterManager>().takeHit(70, 100);
                break;
        }
    }

    public void nextMove(){
        Debug.Log("enemy triggers fight manager");
        FightManager.GetComponent<FightManager>().readyFight(true, false);
    }

    public void attackPlayer(){
        FightManager.GetComponent<FightManager>().readyFight(false, false);
        Debug.Log("enemy attacks player");
        if(gameObject.name == "enemy-old"){
            anim.SetTrigger("attack");
            currentState = "attack";
        }
        else{
            switch(Random.Range(1, 3)) 
            {
                case 1:
                    anim.SetTrigger("attack1");
                    currentState = "attack1";
                    break;
                case 2:
                    anim.SetTrigger("attack2");
                    currentState = "attack2";
                    break;
            }
        }
    }

}
