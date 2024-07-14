using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Importar el namespace para UI

public class juego : MonoBehaviour
{
    public Sprite[] Niveles;

    public GameObject MenuGanar;
    public GameObject PiezaSeleccionada;
    public Image NivelImage; // Referencia al componente Image

    int capa = 1;
    public int PiezasEncajadas = 0;

    void Start()
    {
        int nivelActual = PlayerPrefs.GetInt("Nivel");

        // Configurar la imagen del nivel en el componente Image
        if (NivelImage != null && Niveles.Length > 0 && nivelActual < Niveles.Length)
        {
            NivelImage.sprite = Niveles[nivelActual];
        }

        for (int i = 0; i < 36; i++)
        {
            GameObject.Find("Pieza (" + i + ")").transform.Find("Puzzle").GetComponent<SpriteRenderer>().sprite = Niveles[nivelActual];
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.transform.CompareTag("Puzzle"))
            {
                if (!hit.transform.GetComponent<pieza>().Encajada)
                {
                    PiezaSeleccionada = hit.transform.gameObject;
                    PiezaSeleccionada.GetComponent<pieza>().Seleccionada = true;
                    PiezaSeleccionada.GetComponent<SortingGroup>().sortingOrder = capa;
                    capa++;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (PiezaSeleccionada != null)
            {
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
            PlayerPrefs.SetInt("Nivel", PlayerPrefs.GetInt("Nivel") + 1);
        }
        else
        {
            PlayerPrefs.SetInt("Nivel", 0);
        }
        SceneManager.LoadScene("Juego");
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("Menu");
    }
}
