using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextTransition : MonoBehaviour
{
    public GameObject textPanel;      // Panel de texto que contiene las descripciones
    public Button continueButton;     // Botón para continuar
    public string playerTag = "Player";  // Tag del jugador para detectar la colisión

    private TextMeshProUGUI[] messageTexts; // Arreglo para almacenar los textos hijos del panel
    private int currentIndex = 0; // Índice del texto actual
    private bool playerInRange = false; // Bandera para saber si el jugador está en rango

    void Start()
    {
        if (textPanel == null)
        {
            Debug.LogError("TextPanel no ha sido asignado en el inspector.");
            return;
        }

        if (continueButton == null)
        {
            Debug.LogError("El botón de continuar no ha sido asignado en el inspector.");
            return;
        }
        // Obtener los textos hijos del panel
        messageTexts = textPanel.GetComponentsInChildren<TextMeshProUGUI>();

        // Si no se encuentran textos, mostrar un mensaje de error
        if (messageTexts.Length == 0)
        {
            Debug.LogError("No se encontraron textos hijos en el TextPanel.");
            return;
        }

        // Ocultar todos los textos y el panel al inicio
        HideAllTexts();
        textPanel.SetActive(false);

        // Asignar el evento al botón de continuar
        continueButton.onClick.AddListener(OnContinueButtonClick);

        // Deshabilitar el botón al inicio
        continueButton.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Si el jugador entra en el área de colisión
        if (other.CompareTag(playerTag))
        {
            playerInRange = true;
            // Activar el panel y mostrar el primer texto
            textPanel.SetActive(true);
            ShowCurrentText();
            continueButton.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Si el jugador sale del área de colisión
        if (other.CompareTag(playerTag))
        {
            playerInRange = false;
            HideAllTexts();
            textPanel.SetActive(false);
            continueButton.gameObject.SetActive(false);
        }
    }

    void OnContinueButtonClick()
    {
        if (!playerInRange) return;

        // Avanzar al siguiente texto si hay más
        if (currentIndex < messageTexts.Length - 1)
        {
            currentIndex++;
            ShowCurrentText();
        }
        else
        {
            // Deshabilitar el panel y el botón al terminar
            textPanel.SetActive(false);
            continueButton.gameObject.SetActive(false);
        }
    }

    void ShowCurrentText()
    {
        // Ocultar todos los textos
        HideAllTexts();
        // Activar solo el texto actual
        messageTexts[currentIndex].gameObject.SetActive(true);
    }

    void HideAllTexts()
    {
        foreach (var text in messageTexts)
        {
            if (text != null)
                text.gameObject.SetActive(false);
        }
    }
}
