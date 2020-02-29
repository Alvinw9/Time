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
        text.text = "Total Time: " + ScoreVar.score.ToString("F2");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
