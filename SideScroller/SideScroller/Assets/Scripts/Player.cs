using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Player Controller class.
 */

public class Player : MonoBehaviour
{
    public float maxRunSpeed;

    bool isRunning;

    public float speed;
    
    public Vector2 jumpSpeed;

    public bool isOnGround;

    bool canControl = true;
        
    Rigidbody2D rbody;

    Transform selfTransform;

    public Transform SpawnPoint;

    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        selfTransform = transform;
        SpawnPlayerOnPoint();
    }


    void SpawnPlayerOnPoint()
    {
        selfTransform.position = SpawnPoint.position;
    }

    public void CanControl(bool control)
    {
        canControl = control;
    }

    

    void FixedUpdate()
    {

        if(canControl)
        {
            //Store the current horizontal input in the float moveHorizontal.
            float moveHorizontal = Input.GetAxis("Horizontal");

            //Store the current vertical input in the float moveVertical.
            float moveVertical = Input.GetAxis("Vertical");

            if (moveHorizontal > 0)
            {
                //Facing right
                selfTransform.rotation = new Quaternion(0, 0, 0, 0);
            }

            else if (moveHorizontal < 0)
            {
                //Facing left
                selfTransform.rotation = new Quaternion(0, -180, 0, 0);
            }

            //Use the two store floats to create a new Vector2 variable movement.
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);

            //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
            //if (rbody.velocity.magnitude < maxRunSpeed)
           // if(moveHorizontal != 0 || moveVertical != 0)
           // rbody.velocity = (movement * speed);

            if(Input.GetKey(KeyCode.D))
            {
                rbody.velocity = new Vector2(speed, rbody.velocity.y);
            }

            if (Input.GetKey(KeyCode.A))
            {
                rbody.velocity = new Vector2(-speed, rbody.velocity.y);
            }

            if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                rbody.velocity = (jumpSpeed);
                //rbody.AddForce(jumpSpeed);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
        {
            isRunning = true;
            animator.SetInteger("state", 1);
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            isRunning = false;
            animator.SetInteger("state", 0);
            rbody.velocity = Vector2.zero;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetInteger("state", 2);
        }
    }

    void ResetJump()
    {
        if(isRunning)
        {
            animator.SetInteger("state", 1);
        }
        else
        {
            animator.SetInteger("state", 0);
        }
    }

    public void PlayerDie()
    {
        SpawnPlayerOnPoint();
        GameManager.Instance.ResetGame();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == TagTracker.groundTag)
        {
            Invoke("ResetJump", 0.1f);
        }
        if(collision.gameObject.tag == TagTracker.KillTriggerTag)
        {
            PlayerDie();
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == TagTracker.groundTag)
        {
            isOnGround = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == TagTracker.groundTag)
        {
            isOnGround = false;
        }
    }
}
