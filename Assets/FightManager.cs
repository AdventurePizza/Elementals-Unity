using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    private bool playerReady = false;
    private bool enemyReady = true;
    private bool playerDead = false;
    private bool enemyDead = false;


    public void setPlayerDead(){
        playerDead = true;
    }

    public void setEnemyDead(){
        enemyDead = true;
    }

    public void setEnemy(GameObject newEnemy){
        Debug.Log("target acquired") ;
        enemy = newEnemy;
        enemyDead = false;
        enemyReady = true;
    }

    public void readyFight(bool status, bool isPlayer){
        //Debug.Log("ready figth " + status + " isplayer "  + isPlayer  + " player ready "  + playerReady + " enemy ready "  + enemyReady) ;
        if(isPlayer){
            Debug.Log("Player says, setReady" + status  + " player was ready "  + playerReady + " enemy was ready "  + enemyReady) ;
        }else{
            Debug.Log("enemy says, setReady" + status  + " player was ready "  + playerReady + " enemy was ready "  + enemyReady) ;
        }
        if(isPlayer){
            playerReady = status;
        }else{
            enemyReady = status;
        }
        if(playerReady && enemyReady && !playerDead && !enemyDead){
            Debug.Log("______________________________________________");
            Debug.Log("******************* NEXT TURN ***************");
            Debug.Log("______________________________________________");
            switch(Random.Range(1, 3)) 
            {
                case 1:
                    //player move
                    Debug.Log("players move next");
                    player = (GameObject.FindGameObjectsWithTag("Player"))[0];
                    player.GetComponent<CharacterManager>().attackEnemy();
                    //enemy.GetComponent<EnemyManager>().tryDefend();
                    break;
                case 2:
                    //enemy move
                    Debug.Log("enemy move next");
                    enemy.GetComponent<EnemyManager>().attackPlayer();
                    player = (GameObject.FindGameObjectsWithTag("Player"))[0];
                    player.GetComponent<CharacterManager>().tryDefend();
                    break;
                default:

                    break;
            }
        }
    } 

}
