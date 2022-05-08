using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource MpPlayer;
    public AudioClip[] Musics;
    public int currentIndex = 0;
    public string[] names;
    public AudioListener AudioListener;
    public Button button;

    public Sprite muted;
    public Sprite unmuted;
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

    public void mute(){
        AudioListener.pause = !(AudioListener.pause);
        if(AudioListener.pause){
            button.GetComponent<Image>().sprite = muted;
        }else{
            button.GetComponent<Image>().sprite = unmuted;
        }
    }
}
