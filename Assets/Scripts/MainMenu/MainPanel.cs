using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq; // Necesitarás el paquete Newtonsoft.Json para esto

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

    private void Awake()
    {
        volumeFX.onValueChanged.AddListener(ChangeVolumeFX);
        volumeMaster.onValueChanged.AddListener(ChangeVolumeMaster);
    }

    public void PlayTransition()
    {
        SceneManager.LoadScene("LevelTransition");
        // SceneManager.LoadScene(levelName);
    }
    // public void PlayLevel(string levelName)
    // {
    //     SceneManager.LoadScene(levelName);
    //     PlaySoundButton();
    // }
    public void Levels()
    {
        string jsonResponse = FetchJsonResponse(); // Implementa este método para obtener el JSON

        // Asegúrate de que el JSON está bien formado
        jsonResponse = "{\"levels\":" + jsonResponse + "}";
        Debug.Log("LOAD: " + jsonResponse);

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
    public void ExitGame()
    {
        Application.Quit();
        PlaySoundButton();
    }
    public void SetMute()
    {
       if (mute.isOn)
       {
           mixer.GetFloat("VolMaster", out lastVolume);
           mixer.SetFloat("VolMaster", -80);
       }
      else
           mixer.SetFloat("VolMaster", lastVolume);
    }

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

    public void ChangeVolumeMaster(float v)
    {
        mixer.SetFloat("VolMaster", v);
    }
    public void ChangeVolumeFX(float v)
    {
        mixer.SetFloat("VolFX", v);
    }
    public void PlaySoundButton()
    {
        fxSource.PlayOneShot(clickSound);
    }
        // Ejemplo de cómo podrías obtener el JSON
    private string FetchJsonResponse()
    {
        // Este método debe ser implementado para obtener el JSON de tu fuente de datos
        // Por ejemplo, podrías cargarlo desde un archivo o una API web
        return "[{\"level_id\":1,\"completion_status\":0},{\"level_id\":2,\"completion_status\":1}]";
    }
}
