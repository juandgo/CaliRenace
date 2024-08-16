using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint8 : MonoBehaviour
{   GameController gameController;
    public Transform respawnPoint;
    SpriteRenderer spriteRenderer;
    public Sprite passive, active;
    Collider2D coll;
    public void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            gameController.UpdateCheckpoint(respawnPoint.position);
            spriteRenderer.sprite = active;
            // coll.enabled =false;
        }
    }
}
