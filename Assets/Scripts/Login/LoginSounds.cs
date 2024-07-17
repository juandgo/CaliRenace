using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginSounds : MonoBehaviour
{
    [Header("Options")]
    // public Slider volumeFX;
    // public Slider volumeMaster;
    // public Toggle mute;
    public AudioMixer mixer;
    public AudioSource fxSource;
    public AudioClip clickSound;
    private float lastVolume;

    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject createAcount;
    public GameObject title;



    private void Awake()
    {
        // volumeFX.onValueChanged.AddListener(ChangeVolumeFX);
        // volumeMaster.onValueChanged.AddListener(ChangeVolumeMaster);
    }

    public void PlayLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenPanel(GameObject panel)
    {
        mainPanel.SetActive(false);
        createAcount.SetActive(false);
        panel.SetActive(true);
        title.SetActive(!title.activeSelf);
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
}
