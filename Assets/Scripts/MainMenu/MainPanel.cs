using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq; // Necesitarás el paquete Newtonsoft.Json para esto
using UnityEngine.Networking;
using System.Collections;

public class MainPanel : MonoBehaviour
{
    [Header("Options")]
    public Slider volumeFX;
    public Slider volumeMaster;
    public Toggle mute;
    public AudioMixer mixer;
    public AudioSource fxSource;
    public AudioClip clickSound;
    private float lastVolume;

    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject levelSelectPanel;
    public GameObject optionsPanel;
    public GameObject helpPanel;
    public GameObject profilePanel;
    public GameObject editUserPanel;
    private int userId;  // ID del usuario
    private string baseURL = "http://localhost/www/UnityLoginLogoutRegister/save_load_data.php";

    private void Awake()
    {
        userId = PlayerPrefs.GetInt("accountUserId", -1);
        volumeFX.onValueChanged.AddListener(ChangeVolumeFX);
        volumeMaster.onValueChanged.AddListener(ChangeVolumeMaster);
        mute.onValueChanged.AddListener(delegate { SetMute(); });
    }

    // Método para cambiar de escena con una transición (opcional)
    public void PlayTransition()
    {
        SceneManager.LoadScene("LevelTransition");
    }

    // Método para cargar los niveles según el JSON
    public void Levels()
    {
        WWWForm form = new WWWForm();
        Debug.Log(userId);
        form.AddField("action", "load");
        form.AddField("userId", userId);

        UnityWebRequest www = UnityWebRequest.Post(baseURL, form);
        StartCoroutine(FetchJsonResponseCoroutine(www));
    }
    // Método para salir del juego
    public void ExitGame()
    {
        PlaySoundButton();
        Application.Quit();
    }

    // Método para manejar el mute
    public void SetMute()
    {
        if (mute.isOn)
        {
            mixer.GetFloat("VolMaster", out lastVolume);
            mixer.SetFloat("VolMaster", -80); // Reduce volumen al mínimo
        }
        else
        {
            mixer.SetFloat("VolMaster", lastVolume); // Restaura el volumen previo
        }
    }

    // Método para abrir diferentes paneles
    public void OpenPanel(GameObject panel)
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(false);
        levelSelectPanel.SetActive(false);
        helpPanel.SetActive(false);
        profilePanel.SetActive(false);
        editUserPanel.SetActive(false);

        panel.SetActive(true);
        PlaySoundButton();
    }

    // Métodos para cambiar el volumen
    public void ChangeVolumeMaster(float v)
    {
        mixer.SetFloat("VolMaster", v);
    }

    public void ChangeVolumeFX(float v)
    {
        mixer.SetFloat("VolFX", v);
    }

    // Método para reproducir sonido al hacer clic en un botón
    public void PlaySoundButton()
    {
        fxSource.PlayOneShot(clickSound);
    }

    // Ejemplo de cómo podrías obtener el JSON
    private IEnumerator FetchJsonResponseCoroutine(UnityWebRequest www)
    {
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            // Aquí recibirás el JSON de la respuesta del servidor
            string jsonResponse = www.downloadHandler.text;
            jsonResponse = "{\"levels\":" + jsonResponse + "}";

            Debug.Log("LOAD MAIN: " + jsonResponse);

            // Parsear el JSON a un objeto dinámico
            var jsonObject = JObject.Parse(jsonResponse);

            // Acceder al array de niveles
            var levels = jsonObject["levels"] as JArray;

            // Acceder a las propiedades del primer nivel
            var firstLevel = levels[0];

            if ((int)firstLevel["level_id"] == 1 && (int)firstLevel["completion_status"] == 0)
            {
                Debug.Log(firstLevel["level_id"] + " que pasa " + firstLevel["completion_status"]);
                SceneManager.LoadScene("Opening");
            }
            else
            {
                PlaySoundButton();
                SceneManager.LoadScene("Levels");
            }
        }
    }
}
