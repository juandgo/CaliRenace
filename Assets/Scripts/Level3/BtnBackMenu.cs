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
    public void MainMenu()
    {
        Debug.Log("Ir al menu de inicio");
        SceneManager.LoadScene(1);
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
