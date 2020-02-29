using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class timerText : MonoBehaviour
{
    Text text;
    float startGameTime = 60;
    float gameTimer;
    private int twoPlayer = 1;
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
        text.text = "Time: " + gameTimer.ToString("F2");
    }

    // Update is called once per frame
    void Update()
    {
        if (twoPlayer != 1)
        {
            gameTimer -= Time.deltaTime;
            if (gameTimer == 0)
            {
                SceneManager.LoadScene("EndGame");
            }
        } else
        {
            gameTimer += Time.deltaTime;
        }
        text.text = "Timer: " + gameTimer.ToString("F2");

    }

    public void changeTime(float change)
    {
        gameTimer += change;
    }
}
