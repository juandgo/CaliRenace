using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class UserInfo : MonoBehaviour
{
    private string username;
    public TextMeshProUGUI usernameText;
    public TextMeshProUGUI emailText;

    private string getUserInfoUrl = "http://localhost/www/UnityLoginLogoutRegister/index.php";

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
                    usernameText.text = "Username: " + user.username;
                    emailText.text = "Email: " + user.email;
                }
                else
                {
                    Debug.LogError("User not found");
                }
            }
        }
    }

    [System.Serializable]
    public class User
    {
        public string username;
        public string email;
    }
}
