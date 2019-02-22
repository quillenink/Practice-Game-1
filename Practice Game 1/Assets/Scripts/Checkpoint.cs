using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private LevelManager levelManager;

    public GameObject[] localBoxes;
    private int boxCounter;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        boxCounter = localBoxes.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelManager.resetBoxes)
        {
            foreach(GameObject box in localBoxes)
            {
                ResetNextBox(box);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            levelManager.currentCheckpoint = this.gameObject;
            Debug.Log("Checkpoint: " + this.gameObject.name);
        }
    }

    public void ResetNextBox(GameObject box)
    {
        box.transform.position = box.GetComponent<Box>().resetPosition;
        boxCounter -= 1;
        if (boxCounter == 0)
        {
            levelManager.resetBoxes = false;
            boxCounter = localBoxes.Length;
        }
    }
}
