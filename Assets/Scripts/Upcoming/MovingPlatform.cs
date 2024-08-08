using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public float waitDuration = 1f; // Tiempo de espera entre puntos
    Vector3 targetPos;
    MovementController movementController;
    Rigidbody2D rb;
    Vector3 moveDirection;
    Rigidbody2D playerRb;
    public GameObject ways;
    public Transform[] wayPoints;
    int pointIndex;
    int pointCount;
    int direction = 1;

    private void Awake()
    {
        movementController = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>();
        rb = GetComponent<Rigidbody2D>();
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        wayPoints = new Transform[ways.transform.childCount];
        for (int i = 0; i < ways.transform.childCount; i++)
        {
            wayPoints[i] = ways.transform.GetChild(i);
        }
    }

    private void Start()
    {
        pointIndex = 0;
        pointCount = wayPoints.Length;
        targetPos = wayPoints[pointIndex].position;
        DirectionCalculate(); // Calcular la dirección inicial
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * speed;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, targetPos) < 0.05f)
        {
            NextPoint();
        }
    }

    void NextPoint()
    {
        transform.position = targetPos;
        moveDirection =Vector3.zero;

        if (pointIndex == pointCount - 1)
        {
            direction = -1;
        }
        else if (pointIndex == 0)
        {
            direction = 1;
        }

        pointIndex += direction;
        targetPos = wayPoints[pointIndex].position;
        DirectionCalculate();
        StartCoroutine(WaitNextPoint());
    }

    IEnumerator WaitNextPoint(){
        yield return new WaitForSeconds(waitDuration);
        DirectionCalculate();
    }
    void DirectionCalculate()
    {
        moveDirection = (targetPos - transform.position).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            movementController.isOnPlatform = true;
            movementController.platformRb = rb;
            playerRb.gravityScale *= 50;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            movementController.isOnPlatform = false;
            playerRb.gravityScale /= 50;
        }
    }
}
