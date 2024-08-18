using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint8 : MonoBehaviour
{   GameController gameController;
    public Transform respawnPoint;
    SpriteRenderer spriteRenderer;
    public Sprite passive, active;
    Collider2D coll;
    AudioManagerBox audioManager;
    public void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerBox>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            audioManager.PlaySFX(audioManager.checkpoint); //bounce shroom
            gameController.UpdateCheckpoint(respawnPoint.position);
            spriteRenderer.sprite = active;
            // coll.enabled =false;
        }
    }
}
