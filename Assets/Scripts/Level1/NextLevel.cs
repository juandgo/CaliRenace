using UnityEngine;
using UnityEngine.SceneManagement;
using LevelUnlock;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private Level1 lv1;
    [SerializeField] private GameObject panel;
    [SerializeField] private AudioSource winSound;
    public int levelId = 1;  // ID del nivel completado
    private int userId;  // ID del usuario
    private int scoreP;  // Puntaje obtenido desde la base de datos

    private void Start()
    {
        // Obtén el userId de PlayerPrefs
        userId = PlayerPrefs.GetInt("accountUserId", -1);

        if (userId == -1)
        {
            Debug.LogError("No se encontró el ID de usuario guardado.");
        }
        else
        {
            // Cargar el puntaje del usuario desde la base de datos
            SaveLoadData.Instance.LoadData(userId, OnScoreLoaded);
        }
    }

    private void OnScoreLoaded(int loadedScore)
    {
        
        scoreP = loadedScore;
        Debug.Log("Puntaje cargado desde la base de datos: " + scoreP);
        // Aquí puedes actualizar cualquier UI o lógica adicional con el puntaje
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        lv1.fxSource.Stop();
        if (collider.CompareTag("Player"))
        {
            int maxScore = 0;
                if(lv1.GetScore()> scoreP){
                    maxScore = lv1.GetScore();
                }else{
                    maxScore = scoreP;
                }
            // Guarda el nivel completado antes de cambiar de escena
            if (SaveLoadData.Instance != null)
            {
                SaveLoadData.Instance.SaveData(userId, levelId, "1",  maxScore);
                // SaveLoadData.Instance.SaveData(userId, levelId, "1", lv1.GetScore());
                winSound.Play();
            }
            else
            {
                Debug.Log("failure");
            }
            panel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void OkBtn()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("Levels");
    }
}
