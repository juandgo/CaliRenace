using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageTransition : MonoBehaviour
{
    public Transform parentTransform; // Transform del padre que contiene las imágenes
    public float transitionTime = 2f; // Tiempo en segundos entre cada transición

    private Image[] childImages; // Arreglo para almacenar las imágenes hijas del objeto padre
    private int currentIndex = 0; // Índice de la imagen actual

    void Start()
    {
        // Obtener las imágenes hijas (modificado para evitar la imagen padre)
        childImages = GetChildImages();

        // Si no se encuentran imágenes, mostrar un mensaje de error
        if (childImages.Length == 0)
        {
            Debug.LogError("No se encontraron imágenes hijas en el objeto padre.");
            return;
        }

        // Ocultar todas las imágenes al inicio
        HideAllImages();

        // Iniciar la rutina de transición de imágenes
        StartCoroutine(TransitionImages());
    }

    private Image[] GetChildImages()
    {
        // Usar un ciclo for para recorrer los hijos y verificar si son imágenes
        int childCount = parentTransform.childCount;
        Image[] images = new Image[childCount];

        for (int i = 0; i < childCount; i++)
        {
            Transform child = parentTransform.GetChild(i);
            Image image = child.GetComponent<Image>();

            // Si es una imagen, agregarla al arreglo
            if (image != null)
            {
                images[i] = image;
            }
        }

        return images;
    }

    IEnumerator TransitionImages()
    {
        while (true)
        {
            yield return new WaitForSeconds(transitionTime);

            // Avanzar al siguiente índice circularmente
            currentIndex = (currentIndex + 1) % childImages.Length;

            // Mostrar solo la imagen actual
            ShowCurrentImage();
        }
    }

    void ShowCurrentImage()
    {
        // Ocultar todas las imágenes hijas
        HideAllImages();

        // Activar solo la imagen actual
        childImages[currentIndex].gameObject.SetActive(true);
    }

    void HideAllImages()
    {
        foreach (var image in childImages)
        {
            image.gameObject.SetActive(false);
        }
    }
}
