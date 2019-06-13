using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirVent : MonoBehaviour
{

    public bool isToggled;
    public bool ventOn;
    public float ventToggleSpeed;
    private float ventToggleCounter;
    public bool standingOnVent;

    //allowing lever to manipulate toggled vent
    public bool leverOn = true;

    private Player player;

    //vent particle fade in/out
    public float fadeOutSpeed;
    public float fadeInSpeed;
    public GameObject ventParticle;
    public float ventParticleMaxSize;
    private float currentParticleSize;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        ventToggleCounter = ventToggleSpeed;
        standingOnVent = false;
        //leverOn = false;
        VentParticleToggle();
    }

    // Update is called once per frame
    void Update()
    {
        if (leverOn && isToggled)
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
        /*if(ventOn)
        {
            if(currentParticleSize < ventParticleMaxSize)
            {
                currentParticleSize += fadeInSpeed * Time.deltaTime;
                ParticleSystem.MainModule VPMain = ventParticle.main;
                VPMain.startSize = currentParticleSize;
            }
            if(currentParticleSize > ventParticleMaxSize)
            {
                currentParticleSize = ventParticleMaxSize;
            }
        }
        if(!ventOn)
        {
            if(currentParticleSize > 0)
            {
                currentParticleSize -= fadeOutSpeed * Time.deltaTime;
                ParticleSystem.MainModule VPMain = ventParticle.main;
                VPMain.startSize = currentParticleSize;
            }
            if(currentParticleSize < 0)
            {
                currentParticleSize = 0;
            }
        }*/
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
        /*if(collision.gameObject.tag == "Box")
        {
            if (ventOn)
            {
                collision.GetComponent<Box>().onAirVent = true;
            }
        }*/
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.onAirVent = false;
            standingOnVent = false;
        }
        /*if(collision.gameObject.tag == "Box")
        {
            collision.GetComponent<Box>().onAirVent = false;
        }*/
    }

    public void VentParticleToggle()
    {
        if (ventOn)
        {
            ventParticle.SetActive(true);
        }
        if (!ventOn)
        {
            ventParticle.SetActive(false);
            //particles.startSize = 0;

        }
    }

}
