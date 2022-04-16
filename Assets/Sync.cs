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
    void Start()
    {
        Debug.Log("wrong build");
        setCharacters("public");
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
    }

    void setCharacters(string objktIds)
    {

        objktIds = ",Knight.220.430.80%.80%.60%.60%,Hashashin.104.450.60%.80%.60%.60%,Monk.143.333.80%.80%.80%.80%,Priestess.133.333.80%.80%.80%.80%";
        
        /*
        if(objktIds == "public"){
            objktIds = ",Knight.143.17.60%.80%.60%.60%,Knight.220.137.80%.80%.60%.60%";
        }
        */

        //cam.SetActive(false);
        //Object.Destroy(cam, 0);
        
        for(int i = 0; i < guiObjects.Length; i++){
            guiObjects[i].SetActive(true);
        }

    	Debug.Log("t1");
    	Debug.Log(objktIds);
    	string[] objkts = objktIds.Split(',');
    	
    	for(int i = 0; i < objkts.Length; i++){
            string[] attributes = objkts[i].Split('.');

            if(attributes[0]  == "Knight" || attributes[0]  == "Hashashin" || attributes[0]  == "Monk" || attributes[0]  == "Priestess" ){
                Character temp = new Character();
                temp.type = attributes[0];
                temp.attack = int.Parse(attributes[1]);
                temp.health = int.Parse(attributes[2]);
                temp.attack1 = int.Parse(attributes[3].Replace('%', ' '));
                temp.attack2 = int.Parse(attributes[4].Replace('%', ' '));
                temp.attack3 = int.Parse(attributes[5].Replace('%', ' '));
                temp.attackSP = int.Parse(attributes[6].Replace('%', ' '));
                characters.Add(temp);
            }

            Debug.Log("hey: " + attributes[0]);
        }

            if(characters[0].type == "Knight"  ){
                currentChar = Instantiate(knight, new Vector3(0, -0.57f, 0), Quaternion.identity);

            }
            else if (characters[0].type == "Hashashin" ){
                currentChar = Instantiate(hashashin, new Vector3(0, -4.37f, 0), Quaternion.identity);
            }
            else if (characters[0].type == "Monk" ){
                currentChar = Instantiate(monk, new Vector3(0, 0, 0), Quaternion.identity);
            }
            else if(characters[0].type == "Priestess"){
                currentChar = Instantiate(priestess, new Vector3(0, 0, 0), Quaternion.identity);
            }

            textAttack.GetComponent<TMPro.TextMeshProUGUI>().text = "Attack: " + characters[0].attack;
            textHealth.GetComponent<TMPro.TextMeshProUGUI>().text = "Health: " + characters[0].health;
            textAttack1.GetComponent<TMPro.TextMeshProUGUI>().text = "Attack1: " + characters[0].attack1 + "%";
            textAttack2.GetComponent<TMPro.TextMeshProUGUI>().text = "Attack2: " + characters[0].attack2 + "%";
            textAttack3.GetComponent<TMPro.TextMeshProUGUI>().text = "Attack3: " + characters[0].attack3 + "%";
            textAttackSP.GetComponent<TMPro.TextMeshProUGUI>().text = "AttackSP: " + characters[0].attackSP + "%";
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
            testingChar.GetComponent<CharacterManager>().setStats(characters[currentCharIndex].attack, characters[currentCharIndex].health, characters[currentCharIndex].attack1, characters[currentCharIndex].attack2, characters[currentCharIndex].attack3, characters[currentCharIndex].attackSP);

            testingChar.GetComponent<CharacterManager>().startPlaying();
        }
        else{
            currentChar.GetComponent<CharacterManager>().setStats(characters[currentCharIndex].attack, characters[currentCharIndex].health, characters[currentCharIndex].attack1, characters[currentCharIndex].attack2, characters[currentCharIndex].attack3, characters[currentCharIndex].attackSP);
            currentChar.GetComponent<CharacterManager>().startPlaying();
        }

    }

}
