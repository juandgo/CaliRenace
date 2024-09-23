using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private AudioSource collectionSoundEffect;
    [SerializeField] private AudioSource collectCoinSoundEffect;
    [SerializeField] private AudioSource collectHeartSoundEffect;
    [SerializeField] private AudioSource collectDamageSoundEffect;
    [SerializeField] private float pointsApple;
    [SerializeField] private float pointsOrange;
    [SerializeField] private float pointsStrawberry;
    // [SerializeField] private Score score;
    [SerializeField] private Level1 score;

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Apple")){
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            score.AddScore(pointsApple);

        }else if(collision.gameObject.CompareTag("Orange")){
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            score.AddScore(pointsOrange);

        }else if(collision.gameObject.CompareTag("Strawberry")){
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            score.AddScore(pointsStrawberry);
        }else if(collision.gameObject.CompareTag("Heart")){
            collectHeartSoundEffect.Play();
            Destroy(collision.gameObject);
        }else if(collision.gameObject.CompareTag("Enemy")){
            collectDamageSoundEffect.Play();
        }else if(collision.gameObject.CompareTag("Coin")){
            collectCoinSoundEffect.Play();
            Destroy(collision.gameObject);
        }
    }  
}
