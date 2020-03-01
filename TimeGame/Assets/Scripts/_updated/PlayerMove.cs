/*
 * Changes made for jump physics. Works with both players.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public InputKeys keys;

    bool rightHeld = false;
    bool leftHeld = false;
    bool upHeld = false;
    bool downHeld = false;
    bool onFloor = false;

    bool stunned = false;
    float stunTimer = 0f;

    bool jump = false;
    bool jumpHold = false;
    float jumpTimer = 0f;

    public float hitForce = 10.0f;
    public float movementSpeed = 2.0f;
    float initialSpeed = 0f;
    public float jumpSpeed = 5f;

    public Animator anim;

    private int twoPlayer = 1;

    PlayerMove enemy;

    float xVal, yVal, zVal;

    float leftVal, rightVal, upVal, downVal = 0.0f;

    Rigidbody rb = null;

    public SpawnScript newCoin;

    public timerText timer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        initialSpeed = movementSpeed;

        GameObject enemyObj = GameObject.FindGameObjectWithTag("Player2");
        if (enemyObj)
        {
            enemy = enemyObj.GetComponent<PlayerMove>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        stunTimer -= Time.deltaTime;
        if (stunned && stunTimer <= 0f)
        {
            stunned = false;
        }

        // Check inputs
        rightHeld = Input.GetKey(keys.right) ? true : false;
        leftHeld = Input.GetKey(keys.left) ? true : false;
        upHeld = Input.GetKey(keys.up) ? true : false;
        downHeld = Input.GetKey(keys.down) ? true : false;

        if(Input.GetKeyDown(keys.jump) && onFloor)
        {
            jump = true;
            jumpHold = true;
            jumpTimer = 0.6f;
        }
        if (Input.GetKey(keys.jump))
        {
            jumpHold = true;
        }
        if (Input.GetKeyUp(keys.jump))
        {
            jumpHold = false;
        }
    }

    private void FixedUpdate()
    {
        if (stunned)
        {
            return;
        }

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

            if (anim)
            {
                anim.SetBool("grounded", onFloor);
                anim.ResetTrigger("ground");
                anim.SetTrigger("jump");
            }
        }
        
        float xVal = rightVal - leftVal;
        float zVal = upVal - downVal;

        Vector3 yOnly = new Vector3(0f, rb.velocity.y, 0f);
        Vector3 moveDir = new Vector3(xVal, 0f, zVal).normalized * movementSpeed;

        if(moveDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDir, Vector3.up);
        }

        rb.velocity = moveDir + yOnly;

        if (anim)
        {
            anim.SetFloat("speed", moveDir.magnitude);
            anim.SetBool("grounded", onFloor);
            anim.SetBool("stunned", stunned);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MinuteHand" || collision.gameObject.tag == "HourHand" || collision.gameObject.tag == "Hit")
        {
            HitBack(collision.contacts[0].normal);
        }

        if (collision.gameObject.tag == "Floor" && !onFloor)
        {
            if (anim)
            {
                anim.SetTrigger("ground");
            }
            onFloor = true;
        }

        if (collision.gameObject.tag == "Coin")
        {
            if (twoPlayer == 1)
            {
                SlowEnemy();
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

    public void HitBack(Vector3 direction)
    {
        rb.AddForce((Vector3.up + direction) * 4f, ForceMode.Impulse);
        onFloor = false;

        anim.ResetTrigger("ground");
        anim.SetTrigger("hurt");
        anim.SetBool("stunned", true);

        stunned = true;
        stunTimer = 1f;
    }

    private void SlowEnemy()
    {
        if (enemy)
        {
            enemy.Slow();
        }
    }

    public void Slow()
    {
        movementSpeed = initialSpeed / 2f;
    }

    [System.Serializable]
    public class InputKeys
    {
        public KeyCode up = KeyCode.W;
        public KeyCode down = KeyCode.S;
        public KeyCode left = KeyCode.A;
        public KeyCode right = KeyCode.D;

        public KeyCode jump = KeyCode.Space;
    }
}

