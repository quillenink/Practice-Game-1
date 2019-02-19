using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public Dialogue dialogue;
    private DialogueManager dialogueManager;

    //press E to talk
    private bool inRangeToTalk;
    private bool isTalking;
    public Text textBox;
    public RectTransform textBoxTransform;
    public float textBoxOffsetX;
    public float textBoxOffsetY;
    public Camera cam;
    
    //E to talk button fade in and out
    private float textBoxTransparency = 0f;
    public float fadeSpeed;

    //text box background
    public GameObject background;
    public float backgroundOffsetY;

    //NPC name
    public GameObject charName;
    public float charNameOffsetY;
    public Text charNameText;


    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        inRangeToTalk = false;
        textBox.color = new Color(1f, 1f, 1f, textBoxTransparency);
        background.SetActive(false);
        charName.SetActive(false);
        charNameText.text = dialogue.name;
    }

    // Update is called once per frame
    void Update()
    {
        //text box attached to NPC
        textBoxTransform.position = cam.WorldToScreenPoint(transform.position + new Vector3(textBoxOffsetX, textBoxOffsetY));
        background.transform.position = textBoxTransform.position + new Vector3(0f, backgroundOffsetY);
        charName.transform.position = textBoxTransform.position + new Vector3(0f, charNameOffsetY);

        //press E to talk
        if (Input.GetKeyDown(KeyCode.E) && inRangeToTalk)
        {
            if (!dialogueManager.dialogueIsOpen)
            {
                dialogueManager.StartDialogue(dialogue);
            }
            if (dialogueManager.dialogueIsOpen)
            {
                dialogueManager.DisplayNextSentence();
            }
        }

        //E to talk button fade in and out
        if (inRangeToTalk)
        {
            if(textBoxTransparency < 1f)
            {
                textBoxTransparency += fadeSpeed * Time.deltaTime;
                textBox.color = new Color(1f, 1f, 1f, textBoxTransparency);
            }
        }
        if(!inRangeToTalk && textBoxTransparency > 0f)
        {
            textBoxTransparency -= fadeSpeed * Time.deltaTime;
            textBox.color = new Color(1f, 1f, 1f, textBoxTransparency);
        }
        if (textBoxTransparency > 1f)
        {
            textBoxTransparency = 1f;
        }
        if(textBoxTransparency < 0f)
        {
            textBoxTransparency = 0f;
        }

        //background & NPC name on/off toggle
        if (dialogueManager.dialogueIsOpen)
        {
            background.SetActive(true);
            charName.SetActive(true);
        }
        if (!dialogueManager.dialogueIsOpen)
        {
            background.SetActive(false);
            charName.SetActive(false);
        }
    }

    //in range to talk
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            inRangeToTalk = true;
            dialogueManager.dialogueText = textBox;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inRangeToTalk = false;
        }
    }

    
    
}
