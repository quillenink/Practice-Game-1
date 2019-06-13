using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    //text prompt
    public RectTransform textTransform;
    public Text boxText;
    public float textOffsetY;
    public float fadeSpeed;
    private float textTransparency = 0f;
    private Camera cam;

    public bool inRangeToPickup;
    private bool isPickedUp;

    private Rigidbody2D rigidbody;
    private float gravityStore;

    private Player player;
    private PlayerGrabber playerGrabber;

    public float xOffset;
    public float yOffset;

    public Vector3 resetPosition;

    [SerializeField] private float yVelocityTracker;

    //bouncing on air vent
    public float ventForce;
    public bool onAirVent;
    private AirVent airVent;

    //floating box
    public bool floatingBox;
    public bool spawnsFloating;
    public float floatSpeed;
    public float floatDistance;
    private float floatHeight;
    public float floatBuffer;

    void Start()
    {
        player = FindObjectOfType<Player>();
        playerGrabber = FindObjectOfType<PlayerGrabber>();
        rigidbody = GetComponent<Rigidbody2D>();
        gravityStore = rigidbody.gravityScale;
        onAirVent = false;
        resetPosition = transform.position;
        airVent = null;
        if (spawnsFloating)
        {
            floatHeight = this.transform.position.y + floatDistance;
        }
        else
        {
            floatHeight = this.transform.position.y;
        }

        //text prompt
        cam = FindObjectOfType<Camera>();
    }

    void Update()
    {
        //text prompt
        textTransform.position = cam.WorldToScreenPoint(transform.position + new Vector3(0f, textOffsetY));
        boxText.color = new Color(1f, 1f, 1f, textTransparency);

        //pick up/put down
        if (inRangeToPickup)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ToggleBoxPickup();
            }
            //fade in text
            if(textTransparency < 1f && !isPickedUp)
            {
                textTransparency += fadeSpeed * Time.deltaTime;
            }

        }
        //fade out text
        if (!inRangeToPickup || isPickedUp)
        {
            if(textTransparency > 0f)
            {
                textTransparency -= fadeSpeed * Time.deltaTime;
            }
        }

        if (isPickedUp)
        {
            //fall speed fix
            rigidbody.velocity = new Vector2(0f, 0f);
            //transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            if (player.transform.localScale.x > 0f)
            {
                transform.position = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffset);
            }
            if(player.transform.localScale.x < 0f)
            {
                transform.position = new Vector3(player.transform.position.x + -xOffset, player.transform.position.y + yOffset);
            }
        }

        //fixing fall speed
        yVelocityTracker = rigidbody.velocity.y;
        
    }

    private void FixedUpdate()
    {
        if(!isPickedUp)
        {
            if (onAirVent)
            {
                if (airVent.ventOn)
                {
                    SetBoxGravity(0f);
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x, ventForce);
                }
                if (!airVent.ventOn)
                {
                    ResetBoxGravity();
                }
            }
            if (!onAirVent)
            {
                ResetBoxGravity();
            }
        }
        if(!isPickedUp && floatingBox)
        {
            if(transform.position.y < floatHeight)
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, floatSpeed);
            }
            if(transform.position.y > floatHeight + floatBuffer)
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, -floatSpeed);
            }
            if(transform.position.y <= floatHeight + floatBuffer && transform.position.y > floatHeight)
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "AirVent")
        {
            onAirVent = true;
            airVent = collision.GetComponent<AirVent>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //inRangeToPickup = false;
            isPickedUp = false;
        }
        if(collision.gameObject.tag == "AirVent")
        {
            onAirVent = false;
        }
    }

    public void ToggleBoxPickup()
    {
        isPickedUp = !isPickedUp;
        if (isPickedUp)
        {
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
        if (!isPickedUp)
        {
            rigidbody.constraints = RigidbodyConstraints2D.None;
            if (floatingBox)
            {
                floatHeight = this.transform.position.y + floatDistance;
            }
        }
    }

    public void SetBoxGravity(float targetGravity)
    {
        rigidbody.gravityScale = targetGravity;
    }
    public void ResetBoxGravity()
    {
        rigidbody.gravityScale = gravityStore;
    }

}
