using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    //referencias a los 3 reproductores de sonido que usamos
    public AudioSource sfx;
    public AudioSource musica;
    public AudioSource move;
    //clips de audio para usar en SFX
    public AudioClip disparo;
    public AudioClip explosion;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        //seteamos el volumen correspondiente
        sfx.volume = GameManager.Instance.sfxSound;
        move.volume = 0;
        musica.volume = GameManager.Instance.musicSound;
    }
    public void moveSound(bool _move)//cuando nos movemos ponemos el sonido que tenga nuestra variable si no en 0
    {
        if (_move)
            move.volume = GameManager.Instance.sfxSound;
        else
            move.volume = 0;
    }
    public void newEfect(string efecto)//pasamos un codigo string para ejecutar un sonido en concreto
    {
        sfx.Stop();
        switch (efecto)
        {
            case "disparo":
                sfx.clip = disparo;
                break;
            case "explosion":
                sfx.clip = explosion;
                break;
        }
        sfx.Play();
    }
}
