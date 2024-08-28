using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI; // Asegúrate de incluir esta línea si estás trabajando con UI

public class CreditsManager : MonoBehaviour
{
    public Animator creditsAnimator;
    public GameObject endImage; // La imagen que deseas mostrar opcionalmente
    public bool showEndImage = false; // Para controlar si se muestra la imagen o no

    void Start()
    {
        creditsAnimator.SetTrigger("StartCredits");
    }

    public void PlayCredits()
    {
        creditsAnimator.SetTrigger("StartCredits");
    }

    // Este método se llama al final de la animación
    public void OnCreditsEnd()
    {
        if (showEndImage)
        {
            endImage.SetActive(true); // Muestra la imagen
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Levels");
        }
    }
}
