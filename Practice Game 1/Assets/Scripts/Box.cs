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

    void Start()
    {
        player = FindObjectOfType<Player>();
        playerGrabber = FindObjectOfType<PlayerGrabber>();
        rigidbody = GetComponent<Rigidbody2D>();
        gravityStore = rigidbody.gravityScale;
        resetPosition = transform.position;

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
            if(player.transform.localScale.x > 0f)
            {
                transform.position = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffset);
            }
            if(player.transform.localScale.x < 0f)
            {
                transform.position = new Vector3(player.transform.position.x + -xOffset, player.transform.position.y + yOffset);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            inRangeToPickup = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            inRangeToPickup = false;
            isPickedUp = false;
        }
    }

    public void ToggleBoxPickup()
    {
        isPickedUp = !isPickedUp;
    }

}
