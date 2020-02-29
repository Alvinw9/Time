using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{

    public Coin coin;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 originPos = spawnCircle(transform.position, 3.0f, 30.0f);
        Coin newCoin = Instantiate(coin, originPos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 spawnCircle(Vector3 center, float radius, float ang)
    {
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + 1.6f;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad); ;
        return pos;
    }

}
