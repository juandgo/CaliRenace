using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoor : MonoBehaviour
{
    public Transform shootController;
    public float distanceLine;
    public LayerMask layerPlayer;
    public bool playerInRange;

    void Update()
    {
        playerInRange = Physics2D.Raycast(shootController.position, transform.right, distanceLine, layerPlayer);

        if (playerInRange)
        {

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(shootController.position, shootController.position + transform.right * distanceLine);
    }

}
