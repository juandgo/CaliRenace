using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Vector2 startPos;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
    }
    void Start()
    { startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(0,0);
            transform.position = startPos;
        }
        
    }
}
