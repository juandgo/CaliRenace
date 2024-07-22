using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class UserInfo : MonoBehaviour
{
    private string username;
    public TextMeshProUGUI usernameText;
    public TextMeshProUGUI emailText;
    public TextMeshProUGUI sexText;

    [Header("Update User")]
    public TMP_InputField newUsernameInput;
    public TMP_InputField newEmailInput;
    public TMP_Dropdown sexDropdown;
    public TMP_InputField newPasswordInput;
    public TMP_InputField confirmNewPasswordInput;
    public TextMeshProUGUI textInfo;
    

    // public Button updateButton; // Descomentado y añadido

    private string getUserInfoUrl = "http://localhost/www/UnityLoginLogoutRegister/index.php";
    private string updateUserUrl = "http://localhost/www/UnityLoginLogoutRegister/index.php";

    void Start()
    {
        username = PlayerPrefs.GetString("accountUsername", "Guest");

        if (!string.IsNullOrEmpty(username) && username != "Guest")
        {
            StartCoroutine(GetUserInfo(username));
        }
        else
        {
            Debug.LogError("No se encontró el nombre de usuario guardado.");
        }

        // Añadir listener al botón de actualización
        // updateButton.onClick.AddListener(OnUpdateButtonClick);
    }

    IEnumerator GetUserInfo(string username)
    {
        string url = getUserInfoUrl + "?username=" + username;

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.LogError(webRequest.error);
            }
            else
            {
                string jsonResult = webRequest.downloadHandler.text;
                User user = JsonUtility.FromJson<User>(jsonResult);

                if (user != null)
                {
                    username = user.username;
                    usernameText.text = "Usuario: " + user.username;
                    emailText.text = "Correo: " + user.email;
                    sexText.text = "Sexo: " + user.sex;
                }
                else
                {
                    Debug.LogError("User not found");
                }
            }
        }
    }

    public void OnUpdateButtonClick()
    {
        
        string newUsername = username;
        // string newUsername = newUsernameInput.text;
        string newPass = newPasswordInput.text;
        string confirmNewPass = confirmNewPasswordInput.text;
        string newEmail = newEmailInput.text;
        string sex = sexDropdown.options[sexDropdown.value].text;

        if (newPass != confirmNewPass)
        {
            textInfo.text = "Las contraseñas no coinciden.";
            return;
        }

        if (sex == "Seleccione")
        {
            textInfo.text = "Seleccione su sexo.";
            return;
        }

        if (string.IsNullOrEmpty(newUsername) || string.IsNullOrEmpty(newPass) || string.IsNullOrEmpty(newEmail) || string.IsNullOrEmpty(sex))
        {
            textInfo.text = "Todos los campos son requeridos.";
            return;
        }

        StartCoroutine(UpdateUser(newUsername, newPass, newEmail, sex));
    }

    IEnumerator UpdateUser(string username, string password, string email, string sex)
    {
        WWWForm form = new WWWForm();
        form.AddField("updateUsername", username);
        form.AddField("updatePassword", password);
        form.AddField("updateEmail", email);
        form.AddField("updateSex", sex);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(updateUserUrl, form))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.LogError(webRequest.error);
            }
            else
            {
                string response = webRequest.downloadHandler.text;

                if (response == "1")
                {
                    textInfo.text = "Usuario actualizado exitosamente.";
                }
                else if (response == "2")
                {
                    textInfo.text = "Error al actualizar el usuario.";
                }
                else if (response == "3")
                {
                    textInfo.text = "Usuario no encontrado.";
                }
            }
        }
    }

    [System.Serializable]
    public class User
    {
        public string username;
        public string email;
        public string sex;
    }
}
