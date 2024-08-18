using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManagerBox : MonoBehaviour
{
    // public static AudioManager instance;

    // public AudioSource levelMusic, mainMenuMusic, pointerOver, pointerClick;
    [Header("------------ Audio AudioSource ----------")]
    public AudioSource musicSource;
    public AudioSource SFXSource;
    [Header("--------------- Audio Clip --------------")]

    public AudioClip background, death, checkpoint, wallTouch, portalIn, portalOut;
    private void Start(){
        musicSource.clip = background;
        musicSource.Play();
    }
  
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
