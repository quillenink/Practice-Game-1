using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //jump
    public float jumpHeight;
    //public float jumpLength;
    //private float jumpTime;
    //[SerializeField] bool jumping;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    private bool hasDoubleJumped;

    //grounded check
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    [SerializeField] bool grounded;

    //x axis movement
    public float moveSpeed;
    [SerializeField] Rigidbody2D rigidbody;

    //x axis facing
    private float isMoving;

    //air vent
    public float ventForce;
    public bool onAirVent;
    private float gravityStore;

    //debugging
    [SerializeField] float velocityTracker;

    // Start is called before the first frame update
    void Start()
    {
        gravityStore = rigidbody.gravityScale;
        onAirVent = false;
    }

    // Update is called once per frame
    void Update()
    {
        //debugging
        velocityTracker = rigidbody.velocity.x;

        if (grounded)
        {
            hasDoubleJumped = false;
        }

        //x axis facing
        if (transform.position.x != isMoving)
        {
            if(transform.position.x > isMoving)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            } if(transform.position.x < isMoving)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }

        //x axis movement
        if(Input.GetKey(KeyCode.D))
        {
            rigidbody.velocity = new Vector2(moveSpeed, rigidbody.velocity.y);
            isMoving = transform.position.x;
        } if(Input.GetAxisRaw("Horizontal") < 0)
        {
            rigidbody.velocity = new Vector2(-moveSpeed, rigidbody.velocity.y);
            isMoving = transform.position.x;
        }

        //jump
        if (Input.GetKeyDown(KeyCode.Space) && !hasDoubleJumped)
        {
            rigidbody.velocity = Vector2.up * jumpHeight;
            if (!grounded)
            {
                hasDoubleJumped = true;
            }
        }

        /*if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            jumping = true;
            jumpTime = jumpLength;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            jumpTime -= Time.deltaTime * 0.5f;
            Debug.Log(jumpTime);
            if(jumpTime <= 0)
            {
                jumping = false;
                
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumping = false;
        }*/
        
        
    }

    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (rigidbody.velocity.y < 0)
        {
            rigidbody.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        }
        else if (rigidbody.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rigidbody.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
        }

        //air vent
        if (onAirVent)
        {
            rigidbody.gravityScale = 0f;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, ventForce);
        }
        if (!onAirVent)
        {
            rigidbody.gravityScale = gravityStore;
        }

        /*if (jumping)
        {
            rigidbody.AddForce(Vector2.up * (jumpHeight));
            //rigidbody.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        }*/

    }


}
