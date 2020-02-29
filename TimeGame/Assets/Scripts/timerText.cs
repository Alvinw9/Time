using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timerText : MonoBehaviour
{
    Text text;
    float startGameTime = 60;
    float gameTimer;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        gameTimer = startGameTime;
        text.text = "Time: " + gameTimer.ToString("F2");
    }

    // Update is called once per frame
    void Update()
    {
        gameTimer -= Time.deltaTime;
        text.text = "Timer: " + gameTimer.ToString("F2");

        if(gameTimer == 0)
        {

        }

    }

    public void changeTime(float change)
    {
        gameTimer += change;
    }
}
