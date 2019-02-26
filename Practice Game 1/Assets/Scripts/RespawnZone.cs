using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnZone : MonoBehaviour
{

    private LevelManager levelManager;

    public GameObject box;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            levelManager.RespawnPlayer();
        }
        if(collision.gameObject.tag == "Box")
        {
            collision.attachedRigidbody.velocity = new Vector2(0f, 0f);
            collision.transform.position = collision.GetComponent<Box>().resetPosition;
        }
    }
}
