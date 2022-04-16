using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{


    void OnTriggerEnter2D(Collider2D col)
    {
        SceneManager.LoadScene (sceneName:"stage2");
    }
}
