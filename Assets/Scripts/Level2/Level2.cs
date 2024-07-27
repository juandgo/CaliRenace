using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Level2 : MonoBehaviour
{
    public TextMeshProUGUI textMeshLevel;
    private float score;
    private string level = "2";
    void Start()
    {
        textMeshLevel.text = "Nivel: " + level;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public void BackToMainMenu(string levelName)
    // {
        // Destruir datos específicos de esta escena antes de cargar la nueva
    //     CleanupSceneData();

    //     // Mostrar mensaje en la consola
    //     Debug.Log("Ir al menú de inicio");

    //     // Cargar la nueva escena
    //     SceneManager.LoadScene(levelName);
    // }
    public void MainMenu()
    {
        // Destruir datos específicos de esta escena antes de cargar la nueva
        CleanupSceneData();
        // Cargar la nueva escena
        SceneManager.LoadScene("Levels");
    }

    private void CleanupSceneData()
    {
        // Destruir todos los objetos marcados con un tag específico (opcional)
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("AudioManager");
        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
        }

        // Resetear datos específicos
        score = 0;

        // También puedes resetear otros datos globales aquí si es necesario
        // Ejemplo: GlobalGameManager.instance.ResetData();
    }

    void OnDisable()
    {
        // Limpiar los datos si se destruye este objeto (opcional)
        CleanupSceneData();
    }

}
