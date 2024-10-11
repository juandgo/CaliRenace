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
    [SerializeField] private int pointsApple;
    [SerializeField] private int pointsOrange;
    [SerializeField] private int pointsStrawberry;
    // [SerializeField] private Score score;
    [SerializeField] private Level1 lv1;

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Apple")){
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            lv1.AddScore(pointsApple);

        }else if(collision.gameObject.CompareTag("Orange")){
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            lv1.AddScore(pointsOrange);

        }else if(collision.gameObject.CompareTag("Strawberry")){
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            lv1.AddScore(pointsStrawberry);
        }else if(collision.gameObject.CompareTag("Heart")){
            collectHeartSoundEffect.Play();
            Destroy(collision.gameObject);
        }else if(collision.gameObject.CompareTag("Enemy")){
            collectDamageSoundEffect.Play();
        }else if(collision.gameObject.CompareTag("Coin")){
            collectCoinSoundEffect.Play();
            Destroy(collision.gameObject);
        }else if(collision.gameObject.CompareTag("Timer")){
            collectCoinSoundEffect.Play();
            Destroy(collision.gameObject);
        }
    }  
}
