using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Timer timer;
    public SpawnScript spawner;
    public ClockHand[] clockHands;

    float increaseSpeedTimer = 0.0f;

    public static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        //DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        increaseSpeedTimer += Time.deltaTime;

        if (increaseSpeedTimer >= 5.0f)
        {
            foreach (ClockHand hand in clockHands)
            {
                hand.IncreaseSpeed(5.0f);
            }
            increaseSpeedTimer = 0.0f;
        }
    }

    /// <summary>
    /// Adds time left to the game.
    /// </summary>
    /// <param name="amt">Amount to increase.</param>
    public void TimeIncrease(float amt)
    {
        timer.changeTime(amt);
    }
    public void TimeDecrease(float amt)
    {
        timer.changeTime(-amt);
    }

    public void NewCoin()
    {
        spawner.spawnNewCoin();
    }
}
