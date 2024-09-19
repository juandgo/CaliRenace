using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImageTransition : MonoBehaviour
{
    public float transitionTime = 2f;  // Tiempo entre cada transición
    public float fadeDuration = 1f;    // Duración del desvanecimiento
    public Transform parentTransform;  // Padre de las imágenes
    public Transform textParent;       // Padre de los textos
    public string nextScene;           // Nombre de la siguiente escena

    private Image[] childImages;           // Arreglo de imágenes hijas
    private TextMeshProUGUI[] messageTexts; // Arreglo de textos hijos
    private CanvasGroup[] imageCanvasGroups; // Transparencias de las imágenes
    private CanvasGroup[] textCanvasGroups;  // Transparencias de los textos
    private int currentIndex = 0;          // Índice actual
    private Coroutine transitionCoroutine; // Referencia a la rutina de transición

    void Start()
    {
        // Obtener las imágenes y textos hijos
        childImages = GetChildImages();
        messageTexts = GetChildTexts();

        // Crear CanvasGroups para imágenes y textos
        imageCanvasGroups = AddCanvasGroups(childImages);
        textCanvasGroups = AddCanvasGroups(messageTexts);

        if (childImages.Length == 0 || messageTexts.Length == 0)
        {
            Debug.LogError("No se encontraron imágenes o textos.");
            return;
        }

        // Iniciar la primera imagen y texto con desvanecimiento
        StartCoroutine(FadeInCurrentImageText());

        // Iniciar la rutina de transición de imágenes y textos
        transitionCoroutine = StartCoroutine(TransitionImagesTexts());
    }

    private Image[] GetChildImages()
    {
        var imagesList = new System.Collections.Generic.List<Image>();
        int childCount = parentTransform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = parentTransform.GetChild(i);
            Image image = child.GetComponent<Image>();
            if (image != null)
            {
                imagesList.Add(image);
            }
        }
        return imagesList.ToArray();
    }

    private TextMeshProUGUI[] GetChildTexts()
    {
        var textsList = new System.Collections.Generic.List<TextMeshProUGUI>();
        int childCount = textParent.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = textParent.GetChild(i);
            TextMeshProUGUI text = child.GetComponent<TextMeshProUGUI>();
            if (text != null)
            {
                textsList.Add(text);
            }
        }
        return textsList.ToArray();
    }

    private CanvasGroup[] AddCanvasGroups(Graphic[] graphics)
    {
        var canvasGroups = new CanvasGroup[graphics.Length];
        for (int i = 0; i < graphics.Length; i++)
        {
            CanvasGroup cg = graphics[i].gameObject.AddComponent<CanvasGroup>();
            cg.alpha = 0; // Inicialmente invisibles
            canvasGroups[i] = cg;
        }
        return canvasGroups;
    }

    IEnumerator TransitionImagesTexts()
    {
        while (true)
        {
            yield return new WaitForSeconds(transitionTime);

            currentIndex = (currentIndex + 1) % childImages.Length;

            // Desvanecer la imagen y texto actual y mostrar el siguiente
            StartCoroutine(FadeOutPreviousImageText());
            StartCoroutine(FadeInCurrentImageText());

            // Si es la última imagen, cambiar de escena
            if (currentIndex == childImages.Length - 1)
            {
                SceneManager.LoadScene(nextScene);
            }
        }
    }

    IEnumerator FadeInCurrentImageText()
    {
        CanvasGroup currentImageGroup = imageCanvasGroups[currentIndex];
        CanvasGroup currentTextGroup = textCanvasGroups[currentIndex];

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            currentImageGroup.alpha = alpha;
            currentTextGroup.alpha = alpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        currentImageGroup.alpha = 1f;
        currentTextGroup.alpha = 1f;
    }

    IEnumerator FadeOutPreviousImageText()
    {
        int previousIndex = (currentIndex - 1 + childImages.Length) % childImages.Length;

        CanvasGroup previousImageGroup = imageCanvasGroups[previousIndex];
        CanvasGroup previousTextGroup = textCanvasGroups[previousIndex];

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            previousImageGroup.alpha = alpha;
            previousTextGroup.alpha = alpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        previousImageGroup.alpha = 0f;
        previousTextGroup.alpha = 0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextImage();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousImage();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    public void NextImage()
    {
        if (transitionCoroutine != null)
        {
            StopCoroutine(transitionCoroutine);
        }
        currentIndex = (currentIndex + 1) % childImages.Length;
        StartCoroutine(FadeInCurrentImageText());
        transitionCoroutine = StartCoroutine(TransitionImagesTexts());
    }

    public void PreviousImage()
    {
        if (transitionCoroutine != null)
        {
            StopCoroutine(transitionCoroutine);
        }
        currentIndex = (currentIndex - 1 + childImages.Length) % childImages.Length;
        StartCoroutine(FadeInCurrentImageText());
        transitionCoroutine = StartCoroutine(TransitionImagesTexts());
    }
}
