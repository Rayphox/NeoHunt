using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    //referencia a los Sliders
    public Slider music;
    public Slider sfx;
    public Slider Mouse;

    private void Start()
    {
        //seteamos el valor de los sliders como esten por defecto en el game manager
        music.value = GameManager.Instance.musicSound;
        sfx.value = GameManager.Instance.sfxSound;
        Mouse.value = GameManager.Instance.mouseSensitivity;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);//cargamos la scena numero 1 
    }
    public void QuitGame()
    {
        Application.Quit();//cerramos el programa
    }
    public void UpdateDatos()
    {
        //seteamos los valores de las variables a lo que tengan los slider puesto
        GameManager.Instance.musicSound = music.value;
        GameManager.Instance.sfxSound = sfx.value;
        GameManager.Instance.mouseSensitivity = Mouse.value;
    }
}
