using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static SoundManager instance;
    public AudioSource sfx;
    public AudioSource musica;
    public AudioSource move;
    public AudioClip disparo;
    public AudioClip explosion;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
       
    }
}
