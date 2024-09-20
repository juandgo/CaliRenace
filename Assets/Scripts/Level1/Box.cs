using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private GameObject brokenBox;

    public void Destroy(){
        Instantiate(brokenBox, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    
}
