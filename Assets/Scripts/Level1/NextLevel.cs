using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using LevelUnlock;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public int levelId;  // ID del nivel completado
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
                SaveLoadData.Instance.SaveData(userId, 2, "1", score);
            }
            else
            {
                Debug.Log("failure");

            }
            // Cambia la escena al menú principal
            SceneManager.LoadScene("levels");
            // PlaySoundButton();
        }
    }

    // private IEnumerator WaitForSoundAndLoadLevel()
    // {
    //     AudioSource audioSource = GetComponent<AudioSource>();
    //     if (audioSource != null)
    //     {
    //         audioSource.Play();
    //         yield return new WaitForSeconds(audioSource.clip.length);
    //     }

    //     // Asegúrate de tener el nivel actual cargado
    //     if (SaveLoadData.Instance != null)
    //     {

    //     }
    // }
}