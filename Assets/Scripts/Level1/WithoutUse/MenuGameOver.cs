using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MenuGameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenu;
    // private PlayerCombat playerCombat;
    private PlayerLife playerLife;

    private void Start(){
        // playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        // playerCombat.deathPlayer += ActivateMenu;
        playerLife = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();
        playerLife.deathPlayer += ActivateMenu;
    }

    private void ActivateMenu(object sender, EventArgs e){
        gameOverMenu.SetActive(true);
    }


    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu(string nombre){
        SceneManager.LoadScene(nombre);
    }

    public void Quit(){
        // UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
