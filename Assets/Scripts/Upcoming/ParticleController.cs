using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
   [Header("Movement Particle")]
   [SerializeField] ParticleSystem movementParticle;
   [Range(0, 10)]
   [SerializeField] int occurAfterVelocity;
   [Range(0, 0.2f)]
   [SerializeField] float dustFormationPeriod;
   [SerializeField] Rigidbody2D playerRb;
   float counter;
   bool isOnGround;
   [Header("")]
   [SerializeField] ParticleSystem fallParticle;
   [SerializeField] ParticleSystem touchParticle;
   [SerializeField] ParticleSystem playerDeathParticle;
   AudioManagerBox audioManager;

   private void Awake()
   {
      audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerBox>();
   }
   private void Start()
   {
      touchParticle.transform.parent = null;
      fallParticle.transform.parent = null;
      playerDeathParticle.transform.parent = null;
   }
   private void Update()
   {
      counter += Time.deltaTime;
      if (isOnGround && Mathf.Abs(playerRb.velocity.x) > occurAfterVelocity)
      {
         if (counter > dustFormationPeriod)
         {
            movementParticle.Play();
            counter = 0;
         }
      }
   }
   // public void PlayParticle(Particles particle,Vector2 pos = default(Vector2)){

   // }
   public void PlayTouchParticle(Vector2 pos)
   {
      touchParticle.transform.position = pos;
      // play wall touch SFX here
      audioManager.PlaySFX(audioManager.wallTouch); //bounce shroom
      touchParticle.Play();
   }

   public void PlayFallParticle(Vector2 pos)
   {

      fallParticle.transform.position = pos;
      //play fall SFX here
      audioManager.PlaySFX(audioManager.wallTouch); //bounce shroom
      fallParticle.Play();
   }

   public void PlayDeathParticle(Vector2 pos)
   {
      playerDeathParticle.transform.position = pos;
      audioManager.PlaySFX(audioManager.death); //bounce shroom
      //play death SFX here
      playerDeathParticle.Play();
   }
   private void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.CompareTag("Ground"))
      {
         fallParticle.Play();
         isOnGround = true;
      }

   }
   private void OnTriggerExit2D(Collider2D collision)
   {
      if (collision.CompareTag("Ground"))
      {
         isOnGround = false;
      }
   }

}
