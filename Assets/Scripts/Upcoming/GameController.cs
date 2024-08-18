using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameController : MonoBehaviour
{
    Vector2 checkpointPos;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D playerRb;
    public ParticleController particleController;
    private MovementController movementController;
    private ShadowCaster2D shadowCaster;
    private BoxCollider2D boxCollider;
    [SerializeField] private float waitToRespawn;
    [SerializeField] private FlashImage _flashImage = null;
    [SerializeField] private Color _flashColor = Color.white;
    [SerializeField] private float _flashTime, _flashMinAlpha, _flashMmaxAlpha;
    [SerializeField] private AudioSource deathSound; // AudioSource component for the death sound

    private void Awake()
    {
        // Find the Sprite Renderer on this GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Find the Rigidbody2D on this GameObject        
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
            // Play death particle and sound
            particleController.PlayDeathParticle(transform.position);
            if (deathSound != null)
            {
                deathSound.Play(); // Play death sound
            }
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
    }

    IEnumerator Respawn(float duration)
    {
        playerRb.simulated = false;
        playerRb.velocity = Vector2.zero;
        transform.localScale = Vector3.zero;

        yield return new WaitForSeconds(duration);

        // Stop the death sound after the respawn delay
        if (deathSound != null && deathSound.isPlaying)
        {
            deathSound.Stop();
        }

        transform.position = checkpointPos;
        transform.localScale = new Vector3(1, 1, 1);
        playerRb.simulated = true;
    }
 
}
