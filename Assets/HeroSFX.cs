using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSFX : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip footstep;
    public AudioClip attack1;
    public AudioClip fire;
    public AudioClip fire2;
    public AudioClip wind;
    public AudioClip wind2;
    public AudioClip fist;
    public AudioClip earth;
    public AudioClip quake;
    public AudioClip water;
    public AudioClip water2;

    public void playFootstep(){
        audioSource.PlayOneShot(footstep, 0.7F);
    }

    public void playAttack1(){
        audioSource.PlayOneShot(attack1, 1F);
    }
    
    public void playFire(){
        audioSource.PlayOneShot(fire, 1F);
    }

    public void playFire2(){
        audioSource.PlayOneShot(fire2, 1F);
    }

    public void playWind(){
        audioSource.PlayOneShot(wind, 1F);
    }

    public void playWind2(){
        audioSource.PlayOneShot(wind2, 1F);
    }

    public void playFist(){
        audioSource.PlayOneShot(fist, 1F);
    }

    public void playEarh(){
        audioSource.PlayOneShot(earth, 1F);
    }

    public void playQuake(){
        audioSource.PlayOneShot(quake, 1F);
    }

    public void playWater(){
        audioSource.PlayOneShot(water, 1F);
    }

    public void playWater2(){
        audioSource.PlayOneShot(water2, 1F);
    }
}
