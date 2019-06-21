using UnityEngine;

public class PlayerGrabber : MonoBehaviour
{
    public bool inRangeToGrab;

    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            //inRangeToGrab = true;

            collision.GetComponent<Box>().inRangeToPickup = true;
        }
        if (collision.gameObject.tag == "Lever")
        {
            collision.GetComponent<Lever>().inRangeToSwitch = true;
        }
        if(collision.gameObject.tag == "Rope")
        {
            player.inRangeToSwing = true;
            player.ropeLinkToGrab = collision.attachedRigidbody;
        }
        if (collision.gameObject.tag == "NPC")
        {
            collision.GetComponent<NPC>().inRangeToTalk = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            //inRangeToGrab = false;

            collision.GetComponent<Box>().inRangeToPickup = false;
        }
        if (collision.gameObject.tag == "Lever")
        {
            collision.GetComponent<Lever>().inRangeToSwitch = false;
        }
        if (collision.gameObject.tag == "Rope")
        {
            player.inRangeToSwing = false;
            player.ropeLinkToGrab = null;
        }
        if(collision.gameObject.tag == "NPC")
        {
            collision.GetComponent<NPC>().inRangeToTalk = false;
        }
    }

}
