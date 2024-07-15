using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class menu : MonoBehaviour
{
    // public void Jugar(int Nivel)
    // {
    //     PlayerPrefs.SetInt("Nivel", Nivel);
    //     SceneManager.LoadScene("Juego");
    // }
    // public void RedesSociales(string URL)
    // {
    //     Application.OpenURL(URL);
    // }
    public VideoPlayer videoPlayer;
    public VideoClip fadeOut;
    public void Jugar(int Nivel)
    {
        FadeOut();
        PlayerPrefs.SetInt("Nivel", Nivel);
        SceneManager.LoadScene("Juego");
    }
    public void FadeOut()
    {
        videoPlayer.clip = fadeOut;
        videoPlayer.Play();
        videoPlayer.loopPointReached += FadeOutComplete;
    }

    void FadeOutComplete(VideoPlayer vp)
    {
        videoPlayer.loopPointReached -= FadeOutComplete; // Desuscribir el evento para evitar múltiples llamadas
        ChangeLevel.Instance.OnFadeComplete(); // Llamar al método OnFadeComplete de ChangeLevel
        SceneManager.LoadScene("Juego"); // Cargar la escena después de que termine el fade-out
    }
}
