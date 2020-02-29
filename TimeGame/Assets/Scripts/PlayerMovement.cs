using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool rightHeld = false;
    bool leftHeld = false;
    bool upHeld = false;
    bool downHeld = false;

    float xVal, yVal, zVal;

    float leftVal, rightVal, upVal, downVal = 0.0f;

    Rigidbody rb = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rightHeld = Input.GetKey(KeyCode.D) ? true : false;
        leftHeld = Input.GetKey(KeyCode.A) ? true : false;
        upHeld = Input.GetKey(KeyCode.W) ? true : false;
        downHeld = Input.GetKey(KeyCode.S) ? true : false;
    }

    private void FixedUpdate()
    {
        leftVal = leftHeld ? 2.0f : 0.0f;
        rightVal = rightHeld ? 2.0f : 0.0f;
        upVal = upHeld ? 2.0f : 0.0f;
        downVal = downHeld ? 2.0f : 0.0f;
        
        float xVal = rightVal - leftVal;
        float zVal = upVal - downVal;

        rb.velocity = new Vector3(xVal, 0, zVal);
    }
}
