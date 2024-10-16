using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using UnityEngine.EventSystems;
using LevelUnlock;

public class UnityLoginRegister : MonoBehaviour
{
    private const string baseUrl = "http://localhost/www/UnityLoginLogoutRegister/";
    private const string ukey = "accountUsername";
    private const string userIdKey = "accountUserId";

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

    private LevelSystemManager levelSystemManager;

    void Start()
    {
        sexDropdown ??= GameObject.Find("SexDropdown").GetComponent<TMP_Dropdown>();
        sexDropdown.onValueChanged.AddListener(DropdownValueChanged);
        levelSystemManager = FindObjectOfType<LevelSystemManager>();
    }

    void Update()
    {
        HandleTabNavigation();
        HandleEnterKey();
    }

    private void HandleTabNavigation()
    {
        if (!Input.GetKeyDown(KeyCode.Tab)) return;

        var current = EventSystem.current.currentSelectedGameObject?.GetComponent<Selectable>();
        var next = current?.FindSelectableOnDown();
        if (next != null)
        {
            var inputField = next.GetComponent<InputField>();
            inputField?.OnPointerClick(new PointerEventData(EventSystem.current));
            EventSystem.current.SetSelectedGameObject(next.gameObject, new BaseEventData(EventSystem.current));
        }
    }

    private void HandleEnterKey()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (loginPanel.activeSelf)
                AccountLogin();
            else
                AccountRegister();
        }
    }

    private void DropdownValueChanged(int index)
{
    if (index != 0)
    {
        string selectedText = sexDropdown.options[index].text;
        Debug.Log("Selected: " + selectedText);
    }
}

    private void UpdateInfoTexts(string message)
    {
        info.text = message;
        info2.text = message;
    }

    private void TogglePanels(GameObject panelToActivate, GameObject panelToDeactivate)
    {
        panelToActivate.SetActive(true);
        panelToDeactivate.SetActive(false);
        title.SetActive(!title.activeSelf);
    }

    public void AccountRegister()
    {
        string user = username.text, em = email.text, sex = sexDropdown.options[sexDropdown.value].text;
        string pass = password.text, confirmPass = confirmPassword.text;

        if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(em) || string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(confirmPass) || sex == "Seleccione")
        {
            UpdateInfoTexts("Todos los campos son obligatorios.");
            return;
        }
        if (pass != confirmPass)
        {
            UpdateInfoTexts("Las contraseñas no coinciden.");
            return;
        }

        StartCoroutine(RegisterNewAccount(user, pass, em, sex));
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
                yield break;
            }

            ProcessRegisterResponse(www.downloadHandler.text, user);
        }
    }

    private void ProcessRegisterResponse(string responseText, string user)
    {
        switch (responseText)
        {
            case "1":
                UpdateInfoTexts("Usuario " + user + " registrado exitosamente.");
                ClearRegistrationFields();
                TogglePanels(loginPanel, createAcount);
                break;
            case "2":
                UpdateInfoTexts("Error al registrar la cuenta.");
                break;
            case "3":
                UpdateInfoTexts("Este usuario no está disponible. Por favor elige otro.");
                break;
            case "4":
                UpdateInfoTexts("Correo electrónico no válido.");
                break;
            case "5":
                UpdateInfoTexts("La contraseña debe tener al menos 8 caracteres, incluyendo una letra mayúscula, una minúscula, un número y un carácter especial.");
                break;
            default:
                UpdateInfoTexts("Error desconocido.");
                break;
        }
    }

    private void ClearRegistrationFields()
    {
        username.text = email.text = password.text = confirmPassword.text = "";
        sexDropdown.value = 0;
    }

    public void AccountLogin()
    {
        string user = usernameLog.text, pass = passwordLog.text;

        if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
        {
            UpdateInfoTexts("Todos los campos son obligatorios.");
            return;
        }

        StartCoroutine(LoginAccount(user, pass));
    }

    IEnumerator LoginAccount(string user, string pass)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUsername", user);
        form.AddField("loginPassword", pass);

        using (UnityWebRequest www = UnityWebRequest.Post(baseUrl + "index.php", form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error de conexión: " + www.error);
                UpdateInfoTexts("Error de conexión. Intenta de nuevo.");
                yield break;
            }

            ProcessLoginResponse(www.downloadHandler.text, user);
        }
    }

    private void ProcessLoginResponse(string responseText, string user)
    {
        if (responseText.StartsWith("{"))
        {
            User loggedUser = JsonUtility.FromJson<User>(responseText);
            PlayerPrefs.SetString(ukey, user);
            PlayerPrefs.SetInt(userIdKey, loggedUser.userId);
            PlayerPrefs.Save();

            UpdateInfoTexts("Inicio de sesión exitoso.");
            levelSystemManager?.ReloadDataForNewUser(loggedUser.userId);
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            UpdateInfoTexts(responseText == "3" ? "Contraseña incorrecta para el usuario." : "Usuario no encontrado.");
        }
    }

    [System.Serializable]
    public class User
    {
        public int userId;
        public string username;
    }
}
