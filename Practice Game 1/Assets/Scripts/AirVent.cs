using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirVent : MonoBehaviour
{

    public bool isToggled;
    [SerializeField]
    private bool ventOn;
    public float ventToggleSpeed;
    private float ventToggleCounter;
    public GameObject ventParticle;
    public bool standingOnVent;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        ventToggleCounter = ventToggleSpeed;
        standingOnVent = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isToggled)
        {
            
            ventToggleCounter -= Time.deltaTime;
            
            if(ventToggleCounter <= 0)
            {
                ventOn = !ventOn;
                ventToggleCounter = ventToggleSpeed;
                VentParticleToggle();
                if(standingOnVent && ventOn)
                {
                    player.onAirVent = true;
                }
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            
            standingOnVent = true;
            if (ventOn)
            {
                player.onAirVent = true;
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.onAirVent = false;
            standingOnVent = false;
        }
    }

    private void VentParticleToggle()
    {
        if (ventOn)
        {
            ventParticle.SetActive(true);
        }
        if (!ventOn)
        {
            ventParticle.SetActive(false);

        }
    }

}
