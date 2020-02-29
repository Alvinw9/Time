using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public float yValue = 0.01f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, yValue, transform.position.z);
    }
}
