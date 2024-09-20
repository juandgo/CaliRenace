using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
// using System.Numerics;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb2D;
    private float inputX;

    [Header("Movement")]

    private float horizontalMovement = 0f;
    [SerializeField] private float speedMovement;
    [Range(0, 0.3f)][SerializeField] private float smothMovement;
    private Vector3 speed = Vector3.zero;
    private bool lookingRight = true;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask whatGround;
    [SerializeField] private Transform groundController;
    [SerializeField] private Vector3 boxDimensions;
    [SerializeField] private bool onGround;

    private bool jump = false;

    [Header("JumpWall")]
    [SerializeField] private Transform wallController;
    [SerializeField] private Vector3 boxWallDimensions;
    private bool onWall;
    private bool sliding;
    [SerializeField] private float speedSlide, jumpForceWallX, jumpForceWallY, jumpTimeWall;
    private bool jumpingFromWall;

    [Header("Bounce")]
    [SerializeField] private float bounceSpeed;


    [Header("Animation")]
    private Animator animator;

    [Header("Sounds")]

    [SerializeField] private AudioSource jumpSound;

    [Header("PlayerLife")]

    public int actualLife, maxLife, valorPrueba;

    public UnityEvent<int> changeLife;

    public event EventHandler deathPlayer;


    float knockbackForce = 10f;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        actualLife = maxLife;
        changeLife.Invoke(actualLife);
    }

    private void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        horizontalMovement = inputX * speedMovement;

        animator.SetFloat("Horizontal", Mathf.Abs(horizontalMovement));
        animator.SetFloat("SpeedY", rb2D.velocity.y);

        animator.SetBool("Sliding", sliding);

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            jumpSound.Play();
        }

        if (!onGround && onWall && inputX != 0)
        {
            sliding = true;
        }
        else
        {
            sliding = false;
        }
    }

    private void FixedUpdate()
    {
        onGround = Physics2D.OverlapBox(groundController.position, boxDimensions, 0f, whatGround);
        animator.SetBool("onGround", onGround);

        onWall = Physics2D.OverlapBox(wallController.position, boxWallDimensions, 0f, whatGround);

        //Move   
        Move(horizontalMovement * Time.fixedDeltaTime, jump);

        jump = false;

        if (sliding)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, Mathf.Clamp(rb2D.velocity.y, -speedSlide, float.MaxValue));
        }
    }

    private void Move(float move, bool jump)
    {
        if (!jumpingFromWall)
        {
            Vector3 speedObjective = new Vector2(move, rb2D.velocity.y);
            rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, speedObjective, ref speed, smothMovement);

        }

        if (move > 0 && !lookingRight)
        {
            //Turn
            Turn();
        }
        else if (move < 0 && lookingRight)
        {
            //Turn
            Turn();
        }

        if (onGround && jump && !sliding)
        {
            onGround = false;
            rb2D.AddForce(new Vector2(0f, jumpForce));
        }

        if (jump && onWall && sliding)
        {
            //JumpWall
            JumpWall();
        }
    }

    private void JumpWall(){
        onWall = false;
        rb2D.velocity = new Vector2(jumpForceWallX * -inputX, jumpForceWallY);
        StartCoroutine(ChangeJumpWall());
    }

    IEnumerator ChangeJumpWall(){
        jumpingFromWall = true;
        yield return new WaitForSeconds(jumpTimeWall);
        jumpingFromWall = false;
    }

    private void Turn()
    {
        lookingRight = !lookingRight;
        // Vector3 scala = transform.localScale;
        // scala.x *= -1;
        // transform.localScale = scala;
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y + 180, 0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundController.position, boxDimensions);
        Gizmos.DrawWireCube(wallController.position, boxWallDimensions);
    }

    public void Bounce()
    {
        rb2D.velocity = new Vector2(rb2D.velocity.x, bounceSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Calcular dirección de retroceso
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;

            // Aplicar fuerza de retroceso
            rb2D.velocity = knockbackDirection * knockbackForce;
        }
    }

    public void TakeDamage(int amountDamage)
    {
        CinemachineCameraMovement.Instance.MoveCamera(5, 5, 0.5f);
        int temporalLife = actualLife - amountDamage;

        if (temporalLife < 0)
        {
            actualLife = 0;
        }
        else
        {
            actualLife = temporalLife;
        }

        changeLife.Invoke(actualLife);

        if (actualLife <= 0)
        {
            deathPlayer?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject);
        }

    }

    public void HealLife(int amountHeal)
    {
        int temporalLife = actualLife + amountHeal;

        if (temporalLife > maxLife)
        {
            actualLife = maxLife;
        }
        else
        {
            actualLife = temporalLife;
        }
        changeLife.Invoke(actualLife);
    }

}
