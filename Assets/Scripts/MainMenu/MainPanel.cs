using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public void PlayLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    public void SelectLevel()
    {
        SceneManager.LoadScene("SelectLevel");
    }
    //public void PlayLevel(int levelNumber)
    //{
    //    SceneManager.LoadScene(levelNumber);
    //}

    public void ExitGame()
    {
        Application.Quit();
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
}
