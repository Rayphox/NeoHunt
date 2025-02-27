using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public GameObject[] points;//Array con puntos para patrullar
    public GameObject[] enemigos;//Array con enemigos en juego
    public bool endGame = false;//control de que seguimos en partida
    public float timeGame = 420;//tiempo para terminar la partida
    public int pointsGame = 0;//puntos acumulados
    public CanvasManager canvasManager;//referencia a nuestro canvasManager
    public int energia = 100; //energia del disparo
    public float timeEnergy = 10; //tiempo que tarda en empezar a recargar
    public float tiempoEntreRecargas = 1f; //cooldown de la recarga de energia
    private void Awake()
    {
        instance = this;
        points = GameObject.FindGameObjectsWithTag("patrulla");//guardamos en nuestro array todos los puntos
        enemigos = GameObject.FindGameObjectsWithTag("Enemigo");//guardamos en nuestro array todos los enemigos
        Time.timeScale = 1;
    }
    public GameObject getNewPoint()//nos da un nuevo punto para poder patrullar
    {
        int temp = Random.Range(0, points.Length);
        return points[temp];
    }
    private void Update()
    {
        if (!endGame)//mientras el juego no termine 
        {
            timeGame -= Time.deltaTime;//vamos descontando el tiempod e nuestra variable
            if(timeGame <= 0)//si nos quedmaos sin tiempo perdemos
            {
                endGame = true;
                loseGame();
            }
        }

        if (energia < 100)
        {
            timeEnergy -= Time.deltaTime;//al bajar de 100 comienza el cooldown
            if (timeEnergy <= 0)//cuando pase el cooldown
            {
                recuperacionGradualDeEnergia();
            }
        }
    }
    public void winGame()//se llama al ganar
    {
        Time.timeScale = 0;//pausamos el juego alterando la scala de tiempo
        pointsGame += Mathf.RoundToInt(timeGame * 10);//sumamos los puntos restantes
        canvasManager.finishGame(true);//mostramos el panel de fin de juego
    }
    public void loseGame()//se llama al perder
    {
        Time.timeScale = 0;
        canvasManager.finishGame(false);
    }
    public void enemigoDestruido()//se llama cuando se destrulle un enemigo
    {
        Invoke("enemigoDestruidoParte2", 0.1f);

    }
    public void enemigoDestruidoParte2()//comprobamos si ya no quedan enemigos en el juego
    {
        Debug.Log("ENEMIGO MUERTO");
        enemigos = GameObject.FindGameObjectsWithTag("Enemigo");
        if (enemigos.Length == 0)
        {
            Debug.Log("WINGAME");
            winGame();
            endGame = true;
        }
    }
    public void recuperacionGradualDeEnergia()
    {
        do { energia += 3; }
        while (energia > 100);
    }

    IEnumerator cooldownRecargas()
    {
        yield return new WaitForSeconds(tiempoEntreRecargas);
        recuperacionGradualDeEnergia();
    }
}
