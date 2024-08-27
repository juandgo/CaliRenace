using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;

public class ImageTransitionLevel : MonoBehaviour
{
    public float transitionTime = 2f;
    public Transform parentTransform;
    public Transform textParent;
    [SerializeField] private GameObject nivelPanel;

    private Image[] childImages;
    private TextMeshProUGUI[] messageTexts;
    private int currentIndex = 0;
    private Coroutine transitionCoroutine;

    void Start()
    {
        childImages = GetChildImages();
        messageTexts = GetChildTexts();

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

        HideAllImagesTexts();
        ShowCurrentImage();
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

    IEnumerator TransitionImagesTexts()
    {
        while (true)
        {
            yield return new WaitForSeconds(transitionTime);

            currentIndex = (currentIndex + 1) % childImages.Length;

            ShowCurrentImage();

            if (currentIndex == childImages.Length - 1)
            {
                OnTransitionComplete();
                break;
            }
        }
    }

    void ShowCurrentImage()
    {
        HideAllImagesTexts();
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

    void OnTransitionComplete()
    {
        Debug.Log("Transición completa. Continuando con el nivel.");

        StartCoroutine(GetLevelData());
    }

    IEnumerator GetLevelData()
    {
        UnityWebRequest www = UnityWebRequest.Get("URL_TO_YOUR_JSON");  // Reemplaza con la URL de tu JSON
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            string jsonResponse = www.downloadHandler.text;

            // Wrap the JSON array in an object if necessary
            jsonResponse = "{\"levels\":" + jsonResponse + "}";
            Debug.Log("LOAD: " + jsonResponse);

            // Parse JSON to dynamic object
            var jsonObject = JObject.Parse(jsonResponse);

            // Access the array of levels
            var levels = jsonObject["levels"] as JArray;

            // Access the first level's properties
            var firstLevel = levels[0];

            if ((int)firstLevel["level_id"] == 1 && (int)firstLevel["completion_status"] == 1)
            {
                Debug.Log(firstLevel["level_id"] + " que pasa " + firstLevel["completion_status"]);
                if (nivelPanel != null)
                {
                    nivelPanel.SetActive(true);
                    Time.timeScale = 0f;
                }
            }
        }
    }
}
