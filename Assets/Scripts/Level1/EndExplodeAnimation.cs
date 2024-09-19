using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndExplodeAnimation : MonoBehaviour
{
    [SerializeField] private float explodeAnimation;

    private void Start(){
        Destroy(gameObject, explodeAnimation);
    }
}
