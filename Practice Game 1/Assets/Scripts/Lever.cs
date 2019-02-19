using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lever : MonoBehaviour
{

    [SerializeField]
    private bool switchedOn;
    private bool inRangeToSwitch;

    public SpriteRenderer leverSprite;

    public AirVent airVent;

    //E to switch button
    public RectTransform textTransform;
    public Text switchText;
    public float textOffsetY;
    private float textTransparency = 0f;
    public float fadeSpeed;

    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        switchedOn = false;
        switchText.color = new Color(1f, 1f, 1f, textTransparency);
    }

    // Update is called once per frame
    void Update()
    {
        textTransform.position = cam.WorldToScreenPoint(transform.position + new Vector3(0f, textOffsetY));
        if (inRangeToSwitch)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                switchedOn = !switchedOn;
                ToggleSwitch();
            }
            if(textTransparency < 1f)
            {
                textTransparency += fadeSpeed * Time.deltaTime;
                switchText.color = new Color(1f, 1f, 1f, textTransparency);
            }
        }
        if (!inRangeToSwitch)
        {
            if(textTransparency > 0f)
            {
                textTransparency -= fadeSpeed * Time.deltaTime;
                switchText.color = new Color(1f, 1f, 1f, textTransparency);
            }
        }
        if (textTransparency > 1f)
        {
            textTransparency = 1f;
        }
        if (textTransparency < 0f)
        {
            textTransparency = 0f;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            inRangeToSwitch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            inRangeToSwitch = false;
        }
    }

    private void ToggleSwitch()
    {
        if (switchedOn)
        {
            leverSprite.flipX = true;
            airVent.ventOn = true;
            airVent.VentParticleToggle();
        }
        if (!switchedOn)
        {
            leverSprite.flipX = false;
            airVent.ventOn = false;
            airVent.VentParticleToggle();
        }
    }
}
