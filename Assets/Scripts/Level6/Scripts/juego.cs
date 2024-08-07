using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Importar el namespace para UI
using LevelUnlock;

public class juego : MonoBehaviour
{
    public Sprite[] Niveles;
    public GameObject MenuGanar;
    public GameObject PiezaSeleccionada;
    public Image NivelImage; // Referencia al componente Image
    public int levelId=6;
    public int score;
    private int userId;
    int capa = 1;
    public int PiezasEncajadas = 0;

    void Start()
    {
        int nivelActual = PlayerPrefs.GetInt("Nivel");

          userId = PlayerPrefs.GetInt("accountUserId", -1);
        if (userId == -1)
        {
            Debug.LogError("No se encontró el ID de usuario guardado.");
        }
        // Configurar la imagen del nivel en el componente Image
        if (NivelImage != null && Niveles.Length > 0 && nivelActual < Niveles.Length)
        {
            NivelImage.sprite = Niveles[nivelActual];
        }
        for (int i = 0; i < 36; i++)
        {
            GameObject.Find("Pieza (" + i + ")").transform.Find("Puzzle").GetComponent<SpriteRenderer>().sprite = Niveles[PlayerPrefs.GetInt("Nivel")];
        }
    }

  void Update()
{
    if (Input.GetMouseButtonDown(0))
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        
        // Check if hit.transform is not null and has the tag "Puzzle"
        if (hit.transform != null && hit.transform.CompareTag("Puzzle"))
        {
            var piezaComponent = hit.transform.GetComponent<pieza>();
            if (piezaComponent != null && !piezaComponent.Encajada)
            {
                PiezaSeleccionada = hit.transform.gameObject;
                piezaComponent.Seleccionada = true;
                hit.transform.GetComponent<SortingGroup>().sortingOrder = capa;
                capa++;
            }
        }
    }

    if (Input.GetMouseButtonUp(0))
    {
        if (PiezaSeleccionada != null)
        {
                // Play the drop sound
                AudioSource audioSource = PiezaSeleccionada.GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    audioSource.Play();
                }

            PiezaSeleccionada.GetComponent<pieza>().Seleccionada = false;
            PiezaSeleccionada = null;
        }
    }

    if (PiezaSeleccionada != null)
    {
        Vector3 raton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        PiezaSeleccionada.transform.position = new Vector3(raton.x, raton.y, 0);
    }

    if (PiezasEncajadas == 36)
    {
        MenuGanar.SetActive(true);
    }
}

    public void SiguienteNivel()
    {
        if (PlayerPrefs.GetInt("Nivel") < Niveles.Length - 1)
        {
            SaveLoadData.Instance.SaveData(userId, levelId, "1", 3);
            // PlayerPrefs.SetInt("Nivel", PlayerPrefs.GetInt("Nivel") + 1);
        }
        else
        {
            PlayerPrefs.SetInt("Nivel", 0);
        }
        // SceneManager.LoadScene("Juego");
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("Levels");
        // SceneManager.LoadScene("Menu");
    }
}