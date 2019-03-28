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
    private float moveVelocity;
    [SerializeField] Rigidbody2D rigidbody;

    //x axis facing
    private float isMoving;

    //air vent
    public float ventForce;
    public bool onAirVent;
    private float gravityStore;

    //swinging from rope
    public Vector2 offsetFromRope;
    public bool inRangeToSwing;
    public bool isSwingingFromRope;
    private HingeJoint2D joint;
    public Rigidbody2D ropeLinkToGrab;


    // Start is called before the first frame update
    void Start()
    {
        gravityStore = rigidbody.gravityScale;
        onAirVent = false;
        inRangeToSwing = false;
        isSwingingFromRope = false;
        joint = gameObject.GetComponent<HingeJoint2D>();
        joint.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //debugging
        moveVelocity = 0f;


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
            //rigidbody.velocity = new Vector2(moveSpeed, rigidbody.velocity.y);
            moveVelocity = moveSpeed;
            isMoving = transform.position.x;
        } if(Input.GetAxisRaw("Horizontal") < 0)
        {
            //rigidbody.velocity = new Vector2(-moveSpeed, rigidbody.velocity.y);
            moveVelocity = -moveSpeed;
            isMoving = transform.position.x;
        }

        rigidbody.velocity = new Vector2(moveVelocity, rigidbody.velocity.y);

        //jump
        if (Input.GetKeyDown(KeyCode.Space) && !hasDoubleJumped)
        {
            rigidbody.velocity = Vector2.up * jumpHeight;
            if (!grounded)
            {
                hasDoubleJumped = true;
            }
        }

        //swinging from rope
        if (isSwingingFromRope)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isSwingingFromRope = false;
                LetGoOfRope();
            }
        }
        if (inRangeToSwing && !isSwingingFromRope)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isSwingingFromRope = true;
                if (isSwingingFromRope)
                {
                    GrabRope(ropeLinkToGrab);
                }
                /*if (!isSwingingFromRope)
                {
                    LetGoOfRope();
                }*/
            }
        }
        

        
        
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

    public void GrabRope(Rigidbody2D ropeLink)
    {
        joint.enabled = true;
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedBody = ropeLink;
        joint.anchor = Vector2.zero;
        joint.connectedAnchor = offsetFromRope;
    }
    public void LetGoOfRope()
    {
        joint.connectedBody = null;
        joint.enabled = false;
        //gameObject.
    }




}
