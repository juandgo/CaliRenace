using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameController : MonoBehaviour
{
    Vector2 checkpointPos;
    private SpriteRenderer spriteRenderer;

    Rigidbody2D playerRb;
    public ParticleController particleController;

    private MovementController movementController;

    private ShadowCaster2D shadowCaster;

    private BoxCollider2D boxCollider;
    [SerializeField] private float waitToRespawn;
    [SerializeField] private FlashImage _flashImage = null;
    [SerializeField] private Color _flashColor = Color.white;
    [SerializeField] private float _flashTime, _flashMinAlpha, _flashMmaxAlpha;

    private void Awake()
    {
        //find the Sprite Rendered on this gameobject
        spriteRenderer = GetComponent<SpriteRenderer>();

        //Find the RigidBody2D on this game object        
        playerRb = GetComponent<Rigidbody2D>();
        movementController = GetComponent<MovementController>();

        shadowCaster = GetComponent<ShadowCaster2D>();

        boxCollider = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        checkpointPos = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstaculo"))
        {
            Die();
        }
    }
    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos;
    }
    private void Die()
    {
        _flashImage.Flash(_flashTime, _flashMinAlpha, _flashMmaxAlpha, _flashColor);
        // particleController.PlayDeathParticle(ParticleController.Particles.die, transform.position);
        StartCoroutine(Respawn(waitToRespawn));
        //    StartCoroutine(Respawn(0.5f));
    }

    IEnumerator Respawn(float duration)
    {
        playerRb.simulated = false;
        playerRb.velocity = new Vector2(0, 0);
        transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(duration);
        transform.position = checkpointPos;
        transform.localScale = new Vector3(1, 1, 1);
        playerRb.simulated = true;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
