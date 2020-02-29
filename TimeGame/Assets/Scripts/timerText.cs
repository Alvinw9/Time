﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class timerText : MonoBehaviour
{
    Text text;
    float startGameTime = 60;
    float gameTimer;
    private int twoPlayer = PlayerSet.numPlayers;
    float twoPlayerStartGameTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();

        if (twoPlayer != 1)
        {
            gameTimer = startGameTime;
        } else
        {
            gameTimer = twoPlayerStartGameTime;
        }
        text.text = "Time: " + gameTimer.ToString("F2") + '\n' + "Total Time: " + ScoreVar.score.ToString("F2");
    }

    // Update is called once per frame
    void Update()
    {
        if (twoPlayer != 1)
        {
            gameTimer -= Time.deltaTime;
            ScoreVar.score += Time.deltaTime;
            text.text = "Timer: " + gameTimer.ToString("F2") + '\n' + "Total Time: " + ScoreVar.score.ToString("F2");
            if (gameTimer <= 0)
            {
                SceneManager.LoadScene("EndGame");
            }
        } else
        {
            gameTimer += Time.deltaTime;
            text.text = "Timer: " + gameTimer.ToString("F2") + '\n' + "Player 1: " + ScoreVar.p1Score.ToString("F2") + '\n' + "Player 2: " + ScoreVar.p2Score.ToString("F2");
            if(gameTimer >= 20)
            {
                SceneManager.LoadScene("EndGame");
            }
        }

    }

    public void changeTime(float change)
    {
        gameTimer += change;
    }
}
