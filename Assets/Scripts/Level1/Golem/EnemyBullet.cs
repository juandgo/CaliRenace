using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    public int damage;

    private void Update() {
        transform.Translate(Time.deltaTime * speed * Vector2.right);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.TryGetComponent(out MateoPlayer playerLife))
        {
            playerLife.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
