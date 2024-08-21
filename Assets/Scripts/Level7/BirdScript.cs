using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// using UnityEngine.Audio;
using LevelUnlock;  // Asegúrate de incluir el namespace de SaveLoadData

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    public LogicScript logic;
    public bool birdIsAlive = true;
    private int userId;  // ID del usuario

    // Start is called before the first frame update
    void Start()
    {
                // Obtén el userId de PlayerPrefs
        userId = PlayerPrefs.GetInt("accountUserId", -1);
        if (userId == -1)
        {
            Debug.LogError("No se encontró el ID de usuario guardado.");
        }
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && birdIsAlive)
        {
            myRigidbody.velocity = Vector2.up * flapStrength;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        logic.gameOver();
        birdIsAlive = false;
    }
    public void MainMenu(string level)
    {
        // Guarda el nivel completado antes de cambiar de escena
        if (SaveLoadData.Instance != null)
        {
            SaveLoadData.Instance.SaveData(userId, 7, "1", 3);
        } else {
            Debug.Log("failure");

        }
        SceneManager.LoadScene("Levels");

            // panel.SetActive(true);
            // Time.timeScale = 0f;
    }
        // Cambia la escena al menú principal
        // SceneManager.LoadScene("Levels");
    public void OkBtn()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Levels");
    }

}
