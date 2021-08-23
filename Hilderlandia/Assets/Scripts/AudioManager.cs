using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    public static AudioManager instance;

    public AudioSource jumpFX;
    public AudioSource dieFX;
    public AudioSource enemydieFX;
    public AudioSource bossdieFX;
    public AudioSource buttonFX;
    public AudioSource trampolineFX;
    public AudioSource collectedFX;
    public AudioSource boxFX;
    public AudioSource fallingplatformFX;

    public AudioSource deathFX;
    public AudioSource win;

    public AudioClip gamemusic;
    public AudioClip bossfight;

    public void Start()
    {
        instance = this;
    }

}
