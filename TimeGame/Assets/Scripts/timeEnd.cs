using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timeEnd : MonoBehaviour
{
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        if (PlayerSet.numPlayers == 0)
        {
            text.text = "Total Time: " + ScoreVar.score.ToString("F2");
            ScoreVar.score = 0;
        }
        else
        {
            text.text = "Player 1: " + ScoreVar.p1Score.ToString("F2") + '\n' + "Player 2: " + ScoreVar.p2Score.ToString("F2");
            ScoreVar.p1Score = 0;
            ScoreVar.p2Score = 0;
            ScoreVar.p1Hit = false;
            ScoreVar.p2Hit = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
