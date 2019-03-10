using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabber : MonoBehaviour
{
    public bool inRangeToGrab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

}
