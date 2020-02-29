/*
 * Changes made for jump physics. Works with both players.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    bool rightHeld = false;
    bool leftHeld = false;
    bool upHeld = false;
    bool downHeld = false;
    bool onFloor = false;
    bool hasBeenHit = false;

    bool jump = false;
    bool jumpHold = false;
    float jumpTimer = 0f;

    public float hitForce = 10.0f;
    public float movementSpeed = 2.0f;
    public float jumpSpeed = 5f;

    private int twoPlayer = 1;

    GameObject target;
    Player2Movement enemy;

    float xVal, yVal, zVal;

    float leftVal, rightVal, upVal, downVal = 0.0f;

    Rigidbody rb = null;

    public SpawnScript newCoin;

    public timerText timer;

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
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, -hitForce * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.12f, transform.position.z);
        }

        KeyCode jumpKey = KeyCode.Space;

        // Check inputs
        rightHeld = Input.GetKey(KeyCode.D) ? true : false;
        leftHeld = Input.GetKey(KeyCode.A) ? true : false;
        upHeld = Input.GetKey(KeyCode.W) ? true : false;
        downHeld = Input.GetKey(KeyCode.S) ? true : false;

        if(Input.GetKeyDown(jumpKey) && onFloor)
        {
            jump = true;
            jumpHold = true;
            jumpTimer = 0.6f;
        }
        if (Input.GetKey(jumpKey))
        {
            jumpHold = true;
        }
        if (Input.GetKeyUp(jumpKey))
        {
            jumpHold = false;
        }

    }

    private void FixedUpdate()
    {
        leftVal = leftHeld ? 1f : 0.0f;
        rightVal = rightHeld ? 1f : 0.0f;
        upVal = upHeld ? 1f : 0.0f;
        downVal = downHeld ? 1f : 0.0f;

        if (jumpHold && !jump && !onFloor && jumpTimer > 0f)
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Force);
            jumpTimer -= Time.deltaTime;
        }

        if(jump)
        {
            rb.AddForce(Vector3.up * jumpSpeed * 0.9f, ForceMode.Impulse);
            jump = false;
            onFloor = false;
        }
        
        float xVal = rightVal - leftVal;
        float zVal = upVal - downVal;

        Vector3 yOnly = new Vector3(0f, rb.velocity.y, 0f);

        rb.velocity = (new Vector3(xVal, 0f, zVal).normalized * movementSpeed) + yOnly;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MinuteHand" || collision.gameObject.tag == "HourHand")
        {
            target = collision.gameObject;
            hasBeenHit = true;
        }

        if (collision.gameObject.tag == "Floor")
        {
            onFloor = true;
        }

        if (collision.gameObject.tag == "Coin")
        {
            if (twoPlayer == 1)
            {
                slowEnemy();
            }
            else
            {
                timer.changeTime(5.0f);
            }
            Destroy(collision.gameObject);
            newCoin.spawnNewCoin();
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

    private void slowEnemy()
    {
        GameObject enemyObj = GameObject.FindGameObjectWithTag("Player2");
        
        if (enemyObj != null)
        {
            enemy = enemyObj.GetComponent<Player2Movement>();
            enemy.movementSpeed = 1.5f;
            StartCoroutine(debuffDuration());
        }
    }

    IEnumerator debuffDuration()
    {
        yield return new WaitForSeconds(5.0f);
        GameObject enemyObj = GameObject.FindGameObjectWithTag("Player2");

        if (enemyObj != null)
        {
            enemy = enemyObj.GetComponent<Player2Movement>();
            enemy.movementSpeed = 2.0f;
        }
    }
}

