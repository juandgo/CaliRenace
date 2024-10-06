using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;
using LevelUnlock; // Asegúrate de incluir el namespace correcto

public class UnityLoginRegister : MonoBehaviour
{
    private string baseUrl = "http://localhost/www/UnityLoginLogoutRegister/";

    [Header("Crear Cuenta")]
    public TMP_InputField username;
    public TMP_InputField email;
    public TMP_Dropdown sexDropdown;
    public TMP_InputField password;
    public TMP_InputField confirmPassword;
    public TextMeshProUGUI info;
    public TextMeshProUGUI info2;

    [Header("Login")]
    public TMP_InputField usernameLog;
    public TMP_InputField passwordLog;

    [Header("Panels")]
    public GameObject loginPanel;
    public GameObject createAcount;
    public GameObject title;

    private string ukey = "accountUsername";
    private string userIdKey = "accountUserId"; // Añadido para el ID del usuario

    private LevelSystemManager levelSystemManager; // Referencia a LevelSystemManager

    void Start()
    {
        if (sexDropdown == null)
        {
            sexDropdown = GameObject.Find("SexDropdown").GetComponent<TMP_Dropdown>();
        }

        // Registra un listener para cuando el valor del dropdown cambie
        sexDropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(sexDropdown);
        });

        // Buscar el objeto LevelSystemManager en la escena
        levelSystemManager = FindObjectOfType<LevelSystemManager>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = null;
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                Selectable current = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();
                if (current != null)
                {
                    next = current.FindSelectableOnDown();
                }
            }

            if (next != null)
            {
                InputField inputField = next.GetComponent<InputField>();
                if (inputField != null) inputField.OnPointerClick(new PointerEventData(EventSystem.current));  // If it's an input field, also set the text caret

                EventSystem.current.SetSelectedGameObject(next.gameObject, new BaseEventData(EventSystem.current));
            }
        }
    }

    void DropdownValueChanged(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
        if (index != 0)
        {
            string selectedText = dropdown.options[dropdown.value].text;
            Debug.Log("Selected: " + selectedText);
        }
    }

    private void UpdateInfoTexts(string newText)
    {
        info.text = newText;
        info2.text = newText;
    }

    public void AccountRegister()
    {
        string user = username.text;
        string em = email.text;
        string sex = sexDropdown.options[sexDropdown.value].text;
        Debug.Log(sex);
        string pass = password.text;
        string confirmPass = confirmPassword.text;

        if (pass != confirmPass)
        {
            UpdateInfoTexts("Las contraseñas no coinciden.");
            return;
        }
        if (sex == "Seleccione")
        {
            UpdateInfoTexts("¿Cúal es su sexo?");
            return;
        }

        StartCoroutine(RegisterNewAccount(user, pass, em, sex));
    }

    public void AccountLogin()
    {
        string user = usernameLog.text;
        string pass = passwordLog.text;
        StartCoroutine(LoginAccount(user, pass));
    }
    

    IEnumerator RegisterNewAccount(string user, string pass, string em, string sex)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", user);
        form.AddField("password", pass);
        form.AddField("email", em);
        form.AddField("sex", sex);

        using (UnityWebRequest www = UnityWebRequest.Post(baseUrl + "index.php", form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error de conexión: " + www.error);
                UpdateInfoTexts("Error de conexión. Intenta de nuevo.");
            }
            else
            {
                string responseText = www.downloadHandler.text;
                UpdateInfoTexts(responseText);

                if (responseText == "1")
                {
                    UpdateInfoTexts("Usuario " + user + " registrado exitosamente.");
                    loginPanel.SetActive(true);
                    createAcount.SetActive(false);
                    title.SetActive(!title.activeSelf);
                    username.text = "";
                    password.text = "";
                    email.text = "";
                    confirmPassword.text = "";
                }
                else if (responseText == "2")
                {
                    UpdateInfoTexts("Error al registrar la cuenta.");
                }
                else if (responseText == "3")
                {
                    UpdateInfoTexts("Este usuario no está disponible. Por favor cree otro usuario.");
                }
                else if (responseText == "4")
                {
                    UpdateInfoTexts("Correo electrónico no válido.");
                }
                else if (responseText == "5")
                {
                    UpdateInfoTexts("La contraseña debe tener al menos 8 caracteres, incluyendo al menos una letra mayúscula, una letra minúscula, un número y un carácter especial.");
                }
                else
                {
                    UpdateInfoTexts("Error desconocido.");
                }
            }
        }
    }

    IEnumerator LoginAccount(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUsername", username);
        form.AddField("loginPassword", password);

        using (UnityWebRequest www = UnityWebRequest.Post(baseUrl + "index.php", form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error de conexión: " + www.error);
                UpdateInfoTexts("Error de conexión. Intenta de nuevo.");
            }
            else
            {
                string responseText = www.downloadHandler.text;
                Debug.Log("Response: " + responseText);

                if (responseText.StartsWith("{")) // Asumir respuesta JSON si es exitosa
                {
                    User user = JsonUtility.FromJson<User>(responseText);
                    PlayerPrefs.SetString(ukey, username);
                    PlayerPrefs.SetInt(userIdKey, user.userId);
                    PlayerPrefs.Save();

                    UpdateInfoTexts("Inicio de sesión exitoso del usuario " + username);
                    
                    // Llamar al método para cargar los datos del usuario
                    if (levelSystemManager != null)
                    {
                        levelSystemManager.ReloadDataForNewUser(user.userId);
                    }

                    SceneManager.LoadScene("MainMenu");
                }
                else if (responseText == "2")
                {
                    UpdateInfoTexts("Contraseña incorrecta para el usuario " + username);
                }
                else if (responseText == "3")
                {
                    UpdateInfoTexts("Usuario " + username + " no encontrado.");
                }
                else
                {
                    UpdateInfoTexts("Error desconocido.");
                }
            }
        }
    }

    [System.Serializable]
    public class User
    {
        public int userId;
        public string username;
    }
}
