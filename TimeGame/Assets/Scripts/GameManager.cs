using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Timer timer;
    public SpawnScript spawner;
    public ClockHand[] clockHands;
    public GameObject soundPrefab;

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

    //******** SOUND

    public void PlaySingle(string soundName)
    {
        if (soundName == "") { return; }
        GameObject fxObj = (GameObject)Instantiate(soundPrefab, Vector3.zero, Quaternion.identity);
        if (GameObject.Find("_fx")) { fxObj.transform.parent = GameObject.Find("_effects").transform; }

        AudioSource asource = fxObj.GetComponent<AudioSource>();
        AudioClip a = (AudioClip)Resources.Load(soundName);
        asource.clip = a;
        fxObj.GetComponent<SelfDestruct>().duration = asource.clip.length;
        asource.spatialBlend = 0f;

        asource.volume = 1f * 0.7f;

        asource.Play();
    }
}
