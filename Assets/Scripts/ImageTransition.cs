using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ImageTransition : MonoBehaviour
{
    public float transitionTime = 2f; // Tiempo en segundos entre cada transición
    public Transform parentTransform; // Transform del padre que contiene las imágenes
    public  Transform textParent;      // Transform del padre para los campos de texto

    private Image[] childImages; // Arreglo para almacenar las imágenes hijas del objeto padre
    private TextMeshProUGUI[] messageTexts; // Arreglo para almacenar los textos hijos del objeto padre
    private int currentIndex = 0; // Índice de la imagen actual

    void Start()
    {
        // Obtener las imágenes y textos hijos
        childImages = GetChildImages();
        messageTexts = GetChildTexts();

        // Si no se encuentran imágenes, mostrar un mensaje de error
        if (childImages.Length == 0)
        {
            Debug.LogError("No se encontraron imágenes hijas en el objeto padre.");
            return;
        }
        if (messageTexts.Length == 0)
        {
            Debug.LogError("No se encontraron textos hijos en el objeto padre.");
            return;
        }

        // Ocultar todas las imágenes y textos al inicio
        HideAllImagesTexts();

        // Iniciar la rutina de transición de imágenes y textos
        StartCoroutine(TransitionImagesTexts());
    }

    private Image[] GetChildImages()
    {
        // Usar una lista para manejar dinámicamente las imágenes encontradas
        var imagesList = new System.Collections.Generic.List<Image>();
        int childCount = parentTransform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = parentTransform.GetChild(i);
            Image image = child.GetComponent<Image>();

            // Si es una imagen, agregarla a la lista
            if (image != null)
            {
                imagesList.Add(image);
            }
        }

        return imagesList.ToArray();
    }

    private TextMeshProUGUI[] GetChildTexts()
    {
        // Usar una lista para manejar dinámicamente los textos encontrados
        var textsList = new System.Collections.Generic.List<TextMeshProUGUI>();
        int childCount = textParent.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = textParent.GetChild(i);
            TextMeshProUGUI text = child.GetComponent<TextMeshProUGUI>();

            // Si es un texto, agregarla a la lista
            if (text != null)
            {
                textsList.Add(text);
            }
        }

        return textsList.ToArray();
    }

    IEnumerator TransitionImagesTexts()
    {
        while (true)
        {
            yield return new WaitForSeconds(transitionTime);

            // Avanzar al siguiente índice circularmente
            currentIndex = (currentIndex + 1) % childImages.Length;

            // Mostrar solo la imagen y texto actual
            ShowCurrentImage();
        }
    }

    void ShowCurrentImage()
    {
        // Ocultar todas las imágenes y textos hijos
        HideAllImagesTexts();
        // Activar solo la imagen y texto actual
        childImages[currentIndex].gameObject.SetActive(true);
        messageTexts[currentIndex].gameObject.SetActive(true);
    }

    void HideAllImagesTexts()
    {
        foreach (var image in childImages)
        {
            if (image != null)
                image.gameObject.SetActive(false);
        }

        foreach (var text in messageTexts)
        {
            if (text != null)
                text.gameObject.SetActive(false);
        }
    }
}
