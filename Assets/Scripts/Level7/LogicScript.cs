using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LevelUnlock;  // Asegúrate de incluir el namespace de SaveLoadData

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public GameObject gameOverScreen;
    private int userId;  // ID del usuario
    [SerializeField] private GameObject panel;
    // [SerializeField] private GameObject gameOverPanel; 
    [ContextMenu("Increase Score")]
    void Start()
    {
        // Obtén el userId de PlayerPrefs
        userId = PlayerPrefs.GetInt("accountUserId", -1);
        if (userId == -1)
        {
            Debug.LogError("No se encontró el ID de usuario guardado.");
        }
    }

    public void addScore(int scoreToAdd)
    {
        playerScore = playerScore + scoreToAdd;
        scoreText.text = playerScore.ToString();
        if (scoreText.text == "10")
        {
             MainMenu();
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void MainMenu()
    {
        // Guarda el nivel completado antes de cambiar de escena
        if (SaveLoadData.Instance != null)
        {
            SaveLoadData.Instance.SaveData(userId, 7, "1", 3);
        } else {
            Debug.Log("failure");

        }

        panel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OkBtn()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Levels");
    }
}
