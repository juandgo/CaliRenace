using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using LevelUnlock;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] public Animator transitionAnim;
    // [SerializeField] private AudioSource winSound;
    public int levelId = 1;  // ID del nivel completado
    public int score;  // Puntaje obtenido en el nivel
    private int userId;  // ID del usuario

    private void Start()
    {
        // Obtén el nivel actual desde el servidor al inicio si es necesario
        // SaveLoadData.Instance.LoadData(userId);
        // Obtén el userId de PlayerPrefs
        userId = PlayerPrefs.GetInt("accountUserId", -1);
        if (userId == -1)
        {
            Debug.LogError("No se encontró el ID de usuario guardado.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            // Guarda el nivel completado antes de cambiar de escena
            if (SaveLoadData.Instance != null)
            {
                SaveLoadData.Instance.SaveData(userId, levelId, "1", 3);
                //WIN
                // winSound.Play();
            }
            else
            {
                Debug.Log("failure");
                // StartCoroutine(LoadLevel());
                SceneManager.LoadSceneAsync("Levels");
            }

            panel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    public void OkBtn()
    {
        Time.timeScale = 1f;
    }
    // private IEnumerator LoadLevel()
    // {
    //     transitionAnim.SetTrigger("Start");
    //     yield return new WaitForSeconds(1);
    //     SceneManager.LoadSceneAsync("Levels");
    //     transitionAnim.SetTrigger("End");
    // }
}