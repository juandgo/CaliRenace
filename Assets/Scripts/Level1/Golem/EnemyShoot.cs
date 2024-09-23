using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Transform shootController;
    public float distanceLine;
    public LayerMask layerPlayer;
    public bool playerInRange;
    public float timeBetweenShoots, timeLastShoots, timeWaitShoot;
    public GameObject enemyBullet;

    void Update()
    {
        playerInRange = Physics2D.Raycast(shootController.position, transform.right, distanceLine, layerPlayer);

        if (playerInRange)
        {
            if (Time.time > timeBetweenShoots + timeLastShoots){
                timeLastShoots = Time.time;
                Invoke(nameof(Shoot), timeBetweenShoots);
            }
        }
    }

    private void Shoot(){
        Instantiate(enemyBullet, shootController.position, shootController.rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(shootController.position, shootController.position + transform.right * distanceLine);
    }

}
