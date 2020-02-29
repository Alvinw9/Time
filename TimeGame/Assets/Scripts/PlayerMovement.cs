using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool rightHeld = false;
    bool leftHeld = false;
    bool upHeld = false;
    bool downHeld = false;
    bool onFloor = false;
    bool hasBeenHit = false;

    bool jump = false;
    float jumpTime = 0;
    float speed = 10.0f;

    GameObject target;

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

        if (hasBeenHit)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, -speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.12f, transform.position.z);
        }

        rightHeld = Input.GetKey(KeyCode.D) ? true : false;
        leftHeld = Input.GetKey(KeyCode.A) ? true : false;
        upHeld = Input.GetKey(KeyCode.W) ? true : false;
        downHeld = Input.GetKey(KeyCode.S) ? true : false;
        if(Input.GetKeyDown(KeyCode.Space) && onFloor)
        {
            jump = true;
        }
        /*else
        {
            jump = false;
        }*/
    }

    private void FixedUpdate()
    {
        leftVal = leftHeld ? 2.0f : 0.0f;
        rightVal = rightHeld ? 2.0f : 0.0f;
        upVal = upHeld ? 2.0f : 0.0f;
        downVal = downHeld ? 2.0f : 0.0f;

        if(jump)
        {
            rb.AddForce(Vector3.up * 15, ForceMode.VelocityChange);
            if(jumpTime == 8)
            {
                jumpTime = 0;
                jump = false;
            }
            jumpTime++;
        }

        if(!onFloor)
        {
            rb.AddForce(Vector3.down * 4, ForceMode.VelocityChange);
        }
        
        float xVal = rightVal - leftVal;
        float zVal = upVal - downVal;

        rb.velocity = new Vector3(xVal, 0, zVal);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MinuteHand")
        {
            target = collision.gameObject;
            hasBeenHit = true;
        }

        if (collision.gameObject.tag == "Floor")
        {
            onFloor = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "MinuteHand")
        {
            //hasBeenHit = false;
        }

        if (collision.gameObject.tag == "Floor")
        {
            onFloor = false;
        }

    }
}

