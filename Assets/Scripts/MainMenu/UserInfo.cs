using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class UserInfo : MonoBehaviour
{
    private string username;
    private int userId;
    public TextMeshProUGUI usernameText;
    public TextMeshProUGUI emailText;
    public TextMeshProUGUI sexText;
    public TextMeshProUGUI level1ScoreText;

    [Header("Update User")]
    public TMP_InputField newUsernameInput;
    public TMP_InputField newEmailInput;
    public TMP_Dropdown sexDropdown;
    public TMP_InputField newPasswordInput;
    public TMP_InputField confirmNewPasswordInput;
    public TextMeshProUGUI textInfo;

    // private string urlData = "http://localhost/www/UnityLoginLogoutRegister/index.php";
    // private string urlData = "http://localhost/www/GameBuiltedWeb/UnityLoginLogoutRegister/index.php";
    private string urlData = "http://GameBuiltedWeb/UnityLoginLogoutRegister/index.php";


    void Start()
    {
        userId = PlayerPrefs.GetInt("accountUserId", -1);

        if (userId != -1)
        {
            StartCoroutine(GetUserInfo(userId));
        }
        else
        {
            Debug.LogError("No se encontró el ID de usuario guardado.");
        }
    }

    IEnumerator GetUserInfo(int userId)
{
    string url = urlData + "?user_id=" + userId;

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
                emailText.text = "Correo: " + user.email;
                sexText.text = "Sexo: " + user.sex;
                usernameText.text = "Usuario: " + user.username;

                // Mostrar puntajes
                Debug.Log("Nivel 1 Score: " + user.level1_score);
                Debug.Log("Nivel 2 Score: " + user.level2_score);

                // Puedes también actualizar el UI con los puntajes si tienes los elementos creados
                level1ScoreText.text = "Puntaje Nivel 1: " + user.level1_score;
                // level2ScoreText.text = "Puntaje Nivel 2: " + user.level2_score;
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
        string newUsername = newUsernameInput.text;
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

        StartCoroutine(UpdateUser(userId, newUsername, newPass, newEmail, sex));
    }

    IEnumerator UpdateUser(int userId, string username, string password, string email, string sex)
    {
        WWWForm form = new WWWForm();
        form.AddField("updateUserId", userId);
        form.AddField("updateUsername", username);
        form.AddField("updatePassword", password);
        form.AddField("updateEmail", email);
        form.AddField("updateSex", sex);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(urlData, form))
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
                    StartCoroutine(GetUserInfo(userId)); // Refresca la información del usuario
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
        public int userId;
        public string username;
        public string email;
        public string sex;
        public int level1_score; // Puntaje del nivel 1
        public int level2_score; // Puntaje del nivel 2, puedes agregar más niveles
    }

}
