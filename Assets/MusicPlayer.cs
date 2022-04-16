using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource MpPlayer;
    public AudioClip[] Musics;
    public int currentIndex = 0;
    public string[] names;
     // Use this for initialization
    void Start () {
        MpPlayer.clip = Musics[currentIndex];
        MpPlayer.loop = false;
        MpPlayer.Play();
        StartCoroutine(WaitForTrackTOend());
    }
 
    IEnumerator WaitForTrackTOend()
    {
        while (MpPlayer.isPlaying)
        {
            yield return new WaitForSeconds(0.01f);
        }
        next();
    }

    public void next(){
        currentIndex++;
        if(Musics.Length == currentIndex){
            currentIndex = 0;
        } 
        Debug.Log("playing: " + names[currentIndex]);
        MpPlayer.clip = Musics[currentIndex];
        MpPlayer.Play();

    }
}
