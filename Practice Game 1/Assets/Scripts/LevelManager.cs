using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject currentCheckpoint;

    private Player player;

    public bool resetBoxes;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        resetBoxes = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        Debug.Log("Player Respawn");
        player.transform.position = currentCheckpoint.transform.position;
        resetBoxes = true;
    }
}
