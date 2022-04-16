using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public bool isPlaying = false;
    public bool isFighting = false;
    public float speed;
    public GameObject enemy;
    public Animator anim;
    private string currentState = "idle"; 
    public GameObject CamHolder;

    private Rigidbody2D m_Rigidbody;
    public GameObject FightManager;
    public int maxHealth;
    public int health;
    public bool dead = false;
	public HealthBar healthBar;
    public GameObject hitInfo;
    public GameObject Camera;
    public int damage = 300;
    public int attack1 = 100;
    public int attack2 = 100;
    public int attack3 = 100;
    public int attackSP = 100;
    public int fightStage = 0;
    public AudioSource audioSource;
    public AudioClip takeHitSFX;
    public AudioClip blockSFX;
    public AudioClip gameover;
    // Start is called before the first frame update
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        FightManager = (GameObject.FindGameObjectsWithTag("FightManager"))[0];
        healthBar.SetMaxHealth(health);
        
        Camera = (GameObject.FindGameObjectsWithTag("MainCamera"))[0];
        Debug.Log("camera ");
        Debug.Log(Camera);
        CamHolder = Camera.transform.parent.gameObject;
        CamHolder.GetComponent<CamFollow>().SetPlayer(gameObject);
        DontDestroyOnLoad(this);
    }


    // when the GameObjects collider arrange for this GameObject to travel to the left of the screen
    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);

        if(col.gameObject.tag == "Enemy"){
            enemy = col.gameObject;
            FightManager.GetComponent<FightManager>().setEnemy(enemy); 
        }

        if(col.gameObject.name == "nextSceneTrigger"){
            gameObject.transform.position = new Vector3(-20, -0.57f, 0);
            health = maxHealth;
            fightStage = 0;
        }

        if(col.gameObject.name == "GameManager"){
            FightManager = (GameObject.FindGameObjectsWithTag("FightManager"))[0];
            healthBar.SetMaxHealth(health);
            
            Camera = (GameObject.FindGameObjectsWithTag("MainCamera"))[0];
            Debug.Log("camera ");
            Debug.Log(Camera);
            CamHolder = Camera.transform.parent.gameObject;
            CamHolder.GetComponent<CamFollow>().SetPlayer(gameObject);
        }
    }

    public void setStats(int attack, int health, int attack1, int attack2, int attack3, int attackSP){
        this.health = health;
        damage = attack;
        healthBar.SetMaxHealth(health);
        this.maxHealth = health;
        this.attack1 = attack1;
        this.attack2 = attack2;
        this.attack3 = attack3;
        this.attackSP = attackSP;
    }

    private void attack(){
        Debug.Log("atack happenning, current state: " + currentState);
        if(currentState != "miss"){
            Camera.GetComponent<CameraShake>().Shake(0.05f, 0.2f); 
        }
        switch(currentState) 
        {
            case "attack1":
                Debug.Log("-" + damage + " (enemy)");
                enemy.GetComponent<EnemyManager>().takeHit(damage, 100);
                break;
            case "attack2":
                Debug.Log("-" + damage * 4/3 + " (enemy)");
                enemy.GetComponent<EnemyManager>().takeHit(damage * 4/3, 100);
                break;
            case "attack3":
                Debug.Log("-" + damage * 3/2 + " (enemy)");
                enemy.GetComponent<EnemyManager>().takeHit(damage * 3/2, 100);
                break;
            case "attackSP":
                Debug.Log("-" + damage * 2 + " (enemy)");
                enemy.GetComponent<EnemyManager>().takeHit(damage * 2, 100);
                break;
            case "miss":
                Debug.Log("unlucky ");
                enemy.GetComponent<EnemyManager>().miss();
                break;
            default:
                //anim.SetTrigger("defend");
                break;
            }
    }

    public void nextMove(){
        Debug.Log("player triggers fight manager");
        FightManager.GetComponent<FightManager>().readyFight(true, true); 
    }

    public void takeHit(int damage, int knockback){
        
            if(currentState != "defend"){
                anim.SetTrigger("takeHit");
                audioSource.PlayOneShot(takeHitSFX, 1);
                health = health - damage;
                GameObject temp = Instantiate(hitInfo, gameObject.transform.position, Quaternion.identity);
                temp.GetComponent<HitInfo>().setText("-" + damage); 
                healthBar.SetHealth(health);
                Camera.GetComponent<CameraShake>().Shake(0.05f, 0.2f); 
                if(health <= 0){
                    if(!dead){
                        audioSource.PlayOneShot(gameover, 1);
                        //anim.SetBool("dead", true);
                        anim.SetTrigger("death"); 
                        dead = true;
                        FightManager.GetComponent<FightManager>().setPlayerDead(); 
                    }

                }
            }else{
                audioSource.PlayOneShot(blockSFX, 1);
                GameObject temp = Instantiate(hitInfo, gameObject.transform.position, Quaternion.identity);
                temp.GetComponent<HitInfo>().setText(" BLOCK "); 
                anim.SetBool("defend", false); 
                currentState = "";
                Camera.GetComponent<CameraShake>().Shake(0.1f, 0.2f); 
            }
        
        //FightManager.GetComponent<FightManager>().readyFight(false, true);
        //m_Rigidbody.AddForce(transform.right * knockback * 5);
    }

    public void startPlaying(){
        //anim.SetTrigger("attack2");
        isPlaying = true;
    }

    public void nextEnemy(){
        fightStage++;
        if(fightStage == 2){
            CamHolder.GetComponent<CamFollow>().SetFollow(false);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isPlaying){

            if(gameObject.transform.position.x <= 10.7f){
                anim.SetBool("run", true); 
                gameObject.transform.position = new Vector2 (transform.position.x + (speed), transform.position.y);
                
            }
            else if ( fightStage == 0 && !isFighting) {
                isFighting = true;
                FightManager.GetComponent<FightManager>().readyFight(true, true);
                anim.SetBool("run", false); 
                
            }
            else if (fightStage == 1 && gameObject.transform.position.x <= 27.7f) {
                isFighting = false;
                anim.SetBool("run", true); 
                gameObject.transform.position = new Vector2 (transform.position.x + (speed), transform.position.y);
                
            }
            else if (fightStage == 1 && !isFighting) {
                isFighting = true;
                FightManager.GetComponent<FightManager>().readyFight(true, true);
                anim.SetBool("run", false);  
            }
            else if (fightStage == 2 && gameObject.transform.position.x <= 56f){
                isFighting = false;
                anim.SetBool("run", true); 
                gameObject.transform.position = new Vector2 (transform.position.x + (speed), transform.position.y);
            }

        }
    }

    public void tryDefend(){
        Debug.Log("try defend");
        switch(Random.Range(1, 3)) 
        {
            case 1:
                anim.SetBool("defend", true); 
                currentState = "defend";
                break;
            default:
                break;

        }
    }



    public void attackEnemy(){
        Debug.Log("attack to enemy");
        FightManager.GetComponent<FightManager>().readyFight(false, true);
        switch(Random.Range(1, 5)) 
        {
            case 1:
                anim.SetTrigger("attack1");
                if(attack1 >= Random.Range(1, 100)){
                    currentState = "attack1";
                }else{
                    currentState = "miss";
                }
                break;
            case 2:
                anim.SetTrigger("attack2");
                if(attack2 >= Random.Range(1, 100)){
                    currentState = "attack2";
                }else{
                    currentState = "miss";
                }
                break;
            case 3:
                anim.SetTrigger("attack3");
                if(attack3 >= Random.Range(1, 100)){
                    currentState = "attack3";
                }else{
                    currentState = "miss";
                }
                break;
            case 4:
                anim.SetTrigger("attackSP");
                if(attackSP >= Random.Range(1, 100)){
                    currentState = "attackSP";
                }else{
                    currentState = "miss";
                }
                break;
        }
    }


}
