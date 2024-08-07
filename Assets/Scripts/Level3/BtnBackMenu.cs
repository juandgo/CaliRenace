using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using LevelUnlock;  // Asegúrate de incluir el namespace de SaveLoadData

public class BtnBackMenu : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioSource fxSource;
    public AudioClip clickSound;

    private int levelId=3;  // ID del nivel completado
    public int score;  // Puntaje obtenido en el nivel
    private int userId;  // ID del usuario

    private void Start()
    {
        // Obtén el userId de PlayerPrefs
        userId = PlayerPrefs.GetInt("accountUserId", -1);
        if (userId == -1)
        {
            Debug.LogError("No se encontró el ID de usuario guardado.");
        }
    }

    public void MainMenu(string level)
    {
        Debug.Log($"Select level - userId: {userId}, levelId: {levelId}, score: {score}");
        Debug.Log("Select level");
        
        // Guarda el nivel completado antes de cambiar de escena
        if (SaveLoadData.Instance != null)
        {
            SaveLoadData.Instance.SaveData(userId, levelId, "1", score);
        } else {
            Debug.Log("failure");

        }
        
        // Cambia la escena al menú principal
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
