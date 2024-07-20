using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
                  
public class BtnBackMenu : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioSource fxSource;
    public AudioClip clickSound;
    public void MainMenu(string level)
    {
        Debug.Log("Select level");
        SceneManager.LoadScene(level);
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
