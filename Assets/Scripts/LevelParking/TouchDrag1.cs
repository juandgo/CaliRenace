using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDrag1 : MonoBehaviour
{
    
    float deltaX, deltaY;
    

    bool ColTouched = false;
	
    Rigidbody2D rb;

    void Awake()
    {
		
        rb = GetComponent<Rigidbody2D> ();
		
        rb.isKinematic = true;
    }

    void Update()
    {
		
        if (Input.touchCount > 0) {
			
            Touch touch = Input.GetTouch (0);
				
            Vector3 touchPos = Camera.main.ScreenToWorldPoint (touch.position);
				
            switch (touch.phase) {
					
                case TouchPhase.Began:
						
                    if (GetComponent <Collider2D> () == Physics2D.OverlapPoint (touchPos)) {
							
                        
	                    ColTouched = true;
							
                     
							
                        rb.isKinematic = false;

                        // touch offset
                        deltaX = touchPos.x - transform.position.x;
                        deltaY = touchPos.y - transform.position.y;
                    }
                    break;

					
                case TouchPhase.Moved:

						
                    if (GetComponent<Collider2D> () == Physics2D.OverlapPoint (touchPos) &&ColTouched)
	                    
                        // Diorthosi apo to offset
                        rb.MovePosition (new Vector2 (touchPos.x - deltaX,
                            touchPos.y - deltaY));
                    break;

                
                case TouchPhase.Ended:
	                
                    ColTouched = false;
						
                    rb.isKinematic = true;
                    break;
            }
        }
    }
    
    
    
}
