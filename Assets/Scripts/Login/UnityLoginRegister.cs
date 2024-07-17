using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class UnityLoginRegister : MonoBehaviour
{
    public string baseUrl = "http://localhost/www/UnityLoginLogoutRegister/";

    [Header("Crear Cuenta")]
    public TMP_InputField username;
    public TMP_InputField email;
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

    private void UpdateInfoTexts(string newText)
    {
        info.text = newText;
        info2.text = newText;
    }

    public void AccountRegister()
    {
        string user = username.text;
        string em = email.text;
        string pass = password.text;
        string confirmPass = confirmPassword.text;

        if (pass != confirmPass)
        {
            UpdateInfoTexts("Las contraseñas no coinciden.");
            return;
        }

        StartCoroutine(RegisterNewAccount(user, pass, em));
    }

    public void AccountLogin()
    {
        string user = usernameLog.text;
        string pass = passwordLog.text;
        StartCoroutine(LoginAccount(user, pass));
    }

    IEnumerator RegisterNewAccount(string user, string pass, string em)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", user);
        form.AddField("password", pass);
        form.AddField("email", em);

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
                }else{
                    UpdateInfoTexts("Correo electrónico no válido.");
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

                if (responseText == "1")
                {
                    PlayerPrefs.SetString(ukey, username);
                    UpdateInfoTexts("Inicio de sesión exitoso del usuario " + username);
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
}
