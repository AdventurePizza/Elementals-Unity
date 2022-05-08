using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public struct Character  
{  
    public string type;   
    public int attack;  
    public int health;
    public int attack1;  
    public int attack2;  
    public int attack3;  
    public int attackSP;
}  

public class Sync : MonoBehaviour
{
    public GameObject knight;
    public GameObject hashashin;
    public GameObject monk;
    public GameObject priestess;
    public GameObject textAttack;
    public GameObject textHealth;
    public GameObject textAttack1;
    public GameObject textAttack2;
    public GameObject textAttack3;
    public GameObject textAttackSP;
    private List<Character> characters = new List<Character>();
    private int currentCharIndex = 0;
    private GameObject currentChar;
    
    public GameObject[] guiObjects;
    //public GameObject cam;
    public GameObject testingChar;
    public GameObject[] enemies;

    public AudioSource audioSource;
    public AudioClip changeR;
    public AudioClip changeL;
    public AudioClip select;
    
    public GameObject unplayable;

    void Start()
    {
        //Debug.Log("wrong build");
        //setCharacter("Hashashin.75.3500.40%.60%.40%.60%");
    }

    // when the GameObjects collider arrange for this GameObject to travel to the left of the screen
    void OnTriggerEnter2D(Collider2D col)
    {

        if(col.gameObject.tag == "Player"){
            for(int i = 0; i < enemies.Length; i++){
                enemies[i].SetActive(true);
            }
        }

    }

    public void changeCharacter(bool right){

        
        if(right){
            audioSource.PlayOneShot(changeR, 0.7F);
            currentCharIndex++;
            currentCharIndex = currentCharIndex%characters.Count;
        }else{
            audioSource.PlayOneShot(changeL, 0.7F);
            currentCharIndex--;
            if(currentCharIndex < 0){
                currentCharIndex += characters.Count;
            }
            currentCharIndex = currentCharIndex%characters.Count;
        }
        Destroy(currentChar, 0);
        textAttack.GetComponent<TMPro.TextMeshProUGUI>().text = "Attack: " + characters[currentCharIndex].attack;
        textHealth.GetComponent<TMPro.TextMeshProUGUI>().text = "Health: " + characters[currentCharIndex].health;
        textAttack1.GetComponent<TMPro.TextMeshProUGUI>().text = "Attack1: " + characters[currentCharIndex].attack1 + "%";
        textAttack2.GetComponent<TMPro.TextMeshProUGUI>().text = "Attack2: " + characters[currentCharIndex].attack2 + "%";
        textAttack3.GetComponent<TMPro.TextMeshProUGUI>().text = "Attack3: " + characters[currentCharIndex].attack3 + "%";
        textAttackSP.GetComponent<TMPro.TextMeshProUGUI>().text = "AttackSP: " + characters[currentCharIndex].attackSP + "%";

        if(characters[currentCharIndex].type == "Hashashin"){
            currentChar = Instantiate(hashashin, new Vector3(0, -4.37f, 0), Quaternion.identity);
        }
        else if(characters[currentCharIndex].type == "Knight"){
            currentChar = Instantiate(knight, new Vector3(0, -0.57f, 0), Quaternion.identity);
        }
        else if(characters[currentCharIndex].type == "Monk"){
            currentChar = Instantiate(monk, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else if(characters[currentCharIndex].type == "Priestess"){
            currentChar = Instantiate(priestess, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else{
            Debug.Log(characters[currentCharIndex].type);
        }
/*
        for(int i = 0; i < guiObjects.Length; i++){
            guiObjects[i].SetActive(true);
        }
        */
    }

    void setCharacter(string objktIds)
    {

        for(int i = 0; i < guiObjects.Length; i++){
            guiObjects[i].SetActive(true);
        }

            if(currentChar){
                if(currentChar.GetComponent<CharacterManager>().isPlaying){
                    
                    return;
                }
                Destroy(currentChar, 0);
            }

            string[] attributes = objktIds.Split('.');
            for(int i = 0; i < guiObjects.Length; i++){
                guiObjects[i].SetActive(true);
            }
            unplayable.SetActive(false);
            if(attributes[0] == "Knight"  ){
                currentChar = Instantiate(knight, new Vector3(0, -0.57f, 0), Quaternion.identity);
            }
            else if (attributes[0] == "Hashashin" ){
                currentChar = Instantiate(hashashin, new Vector3(0, -4.37f, 0), Quaternion.identity);
            }
            else if (attributes[0] == "Monk" ){
                currentChar = Instantiate(monk, new Vector3(0, 0, 0), Quaternion.identity);
            }
            else if(attributes[0] == "Priestess"){
                currentChar = Instantiate(priestess, new Vector3(0, 0, 0), Quaternion.identity);
            }else{
                for(int i = 0; i < guiObjects.Length; i++){
                    guiObjects[i].SetActive(false);
                }
                unplayable.SetActive(true);
            }

            textAttack.GetComponent<TMPro.TextMeshProUGUI>().text = "Attack: " + attributes[1];
            textHealth.GetComponent<TMPro.TextMeshProUGUI>().text = "Health: " + attributes[2];
            textAttack1.GetComponent<TMPro.TextMeshProUGUI>().text = "Attack1: " + attributes[3];
            textAttack2.GetComponent<TMPro.TextMeshProUGUI>().text = "Attack2: " + attributes[4];
            textAttack3.GetComponent<TMPro.TextMeshProUGUI>().text = "Attack3: " + attributes[5];
            textAttackSP.GetComponent<TMPro.TextMeshProUGUI>().text = "AttackSP: " + attributes[6];

            currentChar.GetComponent<CharacterManager>().setStats(
                int.Parse(attributes[1]), int.Parse(attributes[2]),
                 int.Parse(attributes[3].Replace('%', ' ')),
                 int.Parse(attributes[4].Replace('%', ' ')),
                 int.Parse(attributes[5].Replace('%', ' ')),
                 int.Parse(attributes[6].Replace('%', ' '))
                 );
            
    }
    public void SelectCharacter()
    {
        audioSource.PlayOneShot(select, 0.7F);
        Debug.Log("selected X");
        for(int i = 0; i < guiObjects.Length; i++){
            guiObjects[i].SetActive(false);
        }
        for(int i = 0; i < enemies.Length; i++){
            enemies[i].SetActive(true);
        }
        //testing scene
        if(testingChar){
            //testingChar.GetComponent<CharacterManager>().setStats(characters[currentCharIndex].attack, characters[currentCharIndex].health, characters[currentCharIndex].attack1, characters[currentCharIndex].attack2, characters[currentCharIndex].attack3, characters[currentCharIndex].attackSP);

            testingChar.GetComponent<CharacterManager>().startPlaying();
        }
        else{
            //currentChar.GetComponent<CharacterManager>().setStats(characters[currentCharIndex].attack, characters[currentCharIndex].health, characters[currentCharIndex].attack1, characters[currentCharIndex].attack2, characters[currentCharIndex].attack3, characters[currentCharIndex].attackSP);
            currentChar.GetComponent<CharacterManager>().startPlaying();
        }

    }

}
