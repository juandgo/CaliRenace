using UnityEngine;

public class PatrullarEnOrden : MonoBehaviour
{
    [SerializeField] private float speedMovement;
    [SerializeField] private Transform[] movementPoints;
    [SerializeField] private float minimunDistance;

    private int nextStep = 0;
    private SpriteRenderer spriteRendrer;

    private void Start()
    {
        spriteRendrer = GetComponent<SpriteRenderer>();
        Turn();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movementPoints[nextStep].position, speedMovement * Time.deltaTime);

        if (Vector2.Distance(transform.position, movementPoints[nextStep].position) < minimunDistance)
        {
            nextStep += 1;
            if (nextStep >= movementPoints.Length)
            {
                nextStep = 0;
            }
            Turn();
        }
    }

    private void Turn()
    {
        if (transform.position.x < movementPoints[nextStep].position.x)
        {
            spriteRendrer.flipX = true;
        }
        else
        {
            spriteRendrer.flipX = false;
        }
    }
}
