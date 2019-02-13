using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Dialogue dialogue;

    //press E to talk
    //private Player player;
    public GameObject talkButton;
    public RectTransform eButtonPrompt;
    public float eButtonOffsetX;
    public float eButtonOffesetY;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        //player = FindObjectOfType<Player>();
        talkButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        eButtonPrompt.position = cam.WorldToScreenPoint(transform.position + new Vector3(eButtonOffsetX, eButtonOffesetY));
            //(cam.WorldToScreenPoint(transform.position));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            talkButton.SetActive(true);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            talkButton.SetActive(false);
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
