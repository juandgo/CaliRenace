using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class UnityLogout : MonoBehaviour
{
    public string baseUrl = "http://localhost/www/UnityLoginLogoutRegister/";

    private string ukey = "accountUsername";

    public void AccountLogout()
    {
        StartCoroutine(LogoutAccount());
    }

    IEnumerator LogoutAccount()
    {
        WWWForm form = new WWWForm();
        form.AddField("logout", "true");

        using (UnityWebRequest www = UnityWebRequest.Post(baseUrl + "index.php", form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error de conexión: " + www.error);
                Debug.Log("Error de conexión. Intenta de nuevo.");
            }
            else
            {
                string responseText = www.downloadHandler.text;
                Debug.Log("Response: " + responseText);

                if (responseText == "Sesión cerrada")
                {
                    PlayerPrefs.SetString(ukey, "");
                    Debug.Log("Has cerrado sesión.");
                    SceneManager.LoadScene("Login");
                }
                else
                {
                    Debug.Log("Error al cerrar sesión.");
                }
            }
        }
    }
}
