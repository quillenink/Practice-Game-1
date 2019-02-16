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

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        ventToggleCounter = ventToggleSpeed;
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
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && ventOn)
        {
            player.onAirVent = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.onAirVent = false;
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
