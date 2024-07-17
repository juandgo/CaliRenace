using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrullar : MonoBehaviour
{
    [SerializeField] private float speedMovement;
    [SerializeField] private Transform[] movementPoints;
    [SerializeField] private float minimunDistance;

    private int randomNumber;
    private SpriteRenderer spriteRendrer;

    private void Start()
    {
        randomNumber = Random.Range(0, movementPoints.Length);
        spriteRendrer = GetComponent<SpriteRenderer>();
        Turn();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movementPoints[randomNumber].position, speedMovement * Time.deltaTime);

        if (Vector2.Distance(transform.position, movementPoints[randomNumber].position) < minimunDistance)
        {
            randomNumber = Random.Range(0, movementPoints.Length);
            //Turn
            Turn();
        }
    }

    private void Turn()
    {
        if (transform.position.x < movementPoints[randomNumber].position.x)
        {
            spriteRendrer.flipX = true;
        }
        else
        {
            spriteRendrer.flipX = false;
        }
    }
}
