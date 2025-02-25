using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public TextMeshProUGUI[] points;
    public TextMeshProUGUI tiempo;
    public GameObject finishGameUI;
    public TextMeshProUGUI finishGameText;

    // Update is called once per frame
    void Update()
    {
        points[0].text = LevelManager.instance.pointsGame.ToString();
        points[1].text = LevelManager.instance.pointsGame.ToString();
        tiempo.text = Mathf.RoundToInt(LevelManager.instance.timeGame).ToString();
    }
    public void finishGame(bool game)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        finishGameUI.SetActive(true);
        if (game)
            finishGameText.text = "WIN";
        else finishGameText.text = "LOSE";
    }
    public void goMenu()
    {
        SceneManager.LoadScene(0);
    }
}
