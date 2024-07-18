using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{
    [Header("Options")]
    public AudioMixer mixer;
    public AudioSource fxSource;
    public AudioClip clickSound;

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
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        PlaySoundButton();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenPanel(GameObject panel)
    {
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
