using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public GameObject[] points;
    public GameObject[] enemigos;
    public bool endGame = false;
    public float timeGame = 420;
    public int pointsGame = 0;
    public CanvasManager canvasManager;
    private void Awake()
    {
        instance = this;
        points = GameObject.FindGameObjectsWithTag("patrulla");
        enemigos = GameObject.FindGameObjectsWithTag("Enemigo");
        Time.timeScale = 1;
    }
    public GameObject getNewPoint()
    {
        int temp = Random.Range(0, points.Length);
        return points[temp];
    }
    private void Update()
    {
        if (!endGame)
        {
            timeGame -= Time.deltaTime;
            if(timeGame <= 0)
            {
                endGame = true;
                loseGame();
            }
        }
    }
    public void winGame()
    {
        Time.timeScale = 0;
        pointsGame += Mathf.RoundToInt(timeGame * 10);
        canvasManager.finishGame(true);
    }
    public void loseGame()
    {
        Time.timeScale = 0;
        canvasManager.finishGame(false);
    }
    public void enemigoDestruido()
    {
        Invoke("enemigoDestruidoParte2", 0.1f);

    }
    public void enemigoDestruidoParte2()
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
}
