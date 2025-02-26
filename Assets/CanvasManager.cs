using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public TextMeshProUGUI[] points;//guardamos los dos textos que contienen los puntos
    public TextMeshProUGUI tiempo;//guardamos el tiempo
    public GameObject finishGameUI;//guardamos el panel que muestra el final del juego
    public TextMeshProUGUI finishGameText;//texto que contiene si ganamos o perdimos 



    // Update is called once per frame
    void Update()
    {
        //mostramos en cada frame los puntos y el tiempo restante
        points[0].text = LevelManager.instance.pointsGame.ToString();
        points[1].text = LevelManager.instance.pointsGame.ToString();
        tiempo.text = Mathf.RoundToInt(LevelManager.instance.timeGame).ToString();
    }
    public void finishGame(bool game)//se llama al terminar la partida el bool determina si ganamos o no
    {
        //permitimos de nuevo el uso del mouse
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        finishGameUI.SetActive(true);//activamos el panel de fin de game y luego seteamos el texto segun como temrino el game
        if (game)
            finishGameText.text = "WIN";
        else finishGameText.text = "LOSE";
    }
    public void goMenu()
    {
        SceneManager.LoadScene(0);//nos carga el menu
    }
}
