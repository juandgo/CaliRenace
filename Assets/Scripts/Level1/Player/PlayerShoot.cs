using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform ShootingContoller;
    [SerializeField] private GameObject bullet;

    private void Update(){
        if(Input.GetButtonDown("Fire1")){
            Shoot();
        }
    }

    private void Shoot(){
        Instantiate(bullet, ShootingContoller.position, ShootingContoller.rotation);
    }
}
