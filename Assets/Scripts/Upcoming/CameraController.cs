using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform target;
    Vector3 velocity = Vector3.zero;
    [Range(0,1)]
    public float smoothTime = 0.2f; // Asignar un valor por defecto para evitar problemas si no se asigna en el Inspector
    public Vector3 positionOffset;

    [Header("Axis Limitation")]
    public Vector2 xLimit; // X axis limitation
    public Vector2 yLimit; // Y axis limitation
    
    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogError("Player not found. Make sure the player GameObject has the 'Player' tag.");
        }
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + positionOffset;
            targetPosition = new Vector3(
                Mathf.Clamp(targetPosition.x, xLimit.x, xLimit.y),
                Mathf.Clamp(targetPosition.y, yLimit.x, yLimit.y), // Sin restar 10
                targetPosition.z // Mantener z sin cambios
            );
            
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
