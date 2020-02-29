using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{

    public Coin coin;
    public bool shouldSpawn = false;
    public PlayerMovement player1;
    public Player2Movement player2;
    private int twoPlayer = PlayerSet.numPlayers;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(delayCoinSpawn());
        PlayerMovement newPlayer1 = Instantiate(player1, new Vector3(1.0f, 1.5f, -2.0f), Quaternion.identity);

        if (twoPlayer == 1)
        {
            Player2Movement newPlayer2 = Instantiate(player2, new Vector3(5.5f, 1.5f, -4.0f), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldSpawn)
        {
            spawnNewCoin();
            shouldSpawn = false;
        }
    }

    public void spawnNewCoin()
    {
        Vector3 originPos = spawnCircle(transform.position, 3.0f, Random.value * 360.0f);
        Coin newCoin = Instantiate(coin, originPos, Quaternion.identity);
    }

    Vector3 spawnCircle(Vector3 center, float radius, float ang)
    {
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + 1.6f;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }

    IEnumerator delayCoinSpawn()
    {
        yield return new WaitForSeconds(3.0f);
        spawnNewCoin();
    }

}
