using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    private bool inRangeToPickup;
    private bool isPickedUp;

    private Rigidbody2D rigidbody;
    private float gravityStore;

    private Player player;

    public float xOffset;
    public float yOffset;

    public Vector3 resetPosition;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        rigidbody = GetComponent<Rigidbody2D>();
        gravityStore = rigidbody.gravityScale;
        resetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //pick up/put down
        if (inRangeToPickup)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ToggleBoxPickup();
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
