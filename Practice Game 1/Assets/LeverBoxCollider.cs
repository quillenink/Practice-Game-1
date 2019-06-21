using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverBoxCollider : MonoBehaviour
{
    public Lever lever;

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
        if(collision.gameObject.tag == "Box")
        {
            if(collision.gameObject.transform.position.y > this.transform.position.y)
            {
                lever.switchedOn = false;
                lever.ToggleSwitch();
            }
            if (collision.gameObject.transform.position.y < this.transform.position.y)
            {
                lever.switchedOn = true;
                lever.ToggleSwitch();
            }
        }
    }
}
