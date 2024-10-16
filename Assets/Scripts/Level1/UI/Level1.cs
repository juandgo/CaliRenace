using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Threading;

public class Level1 : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool pausedGame = false;

    [Header("Sounds")]
    public AudioMixer mixer;
    public AudioSource fxSource;
    public AudioClip clickSound;
    private float lastVolume;

    [SerializeField] private AudioSource gameOverSound;

    [Header("Game Over Menu")]
    [SerializeField] private GameObject gameOverMenu;
    private MateoPlayer playerLife;

    public TextMeshProUGUI textMeshScore;
    public TextMeshProUGUI textMeshLevel;
    private int score;
    private string level = "1";

    private void Start()
    {
        // username = unityLginRegisterInstance.GetName();
        playerLife = GameObject.FindGameObjectWithTag("Player").GetComponent<MateoPlayer>();
        playerLife.deathPlayer += ActivateMenu;
        textMeshLevel.text = "Nivel: " + level;

    }

    private void Update()
    {
        textMeshScore.text = score.ToString("0");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausedGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            if (pausedGame)
            {
                Restart();
            }
        }


    }

    public void Pause()
    {
        PlaySoundButton();
        pausedGame = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        
    }

    public void Resume()
    {
        PlaySoundButton();
        pausedGame = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void Restart()
    {
        PlaySoundButton();
        pausedGame = false;
        Time.timeScale = 1f;
        // Introduce un delay de 2 segundos
        Thread.Sleep(1000);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        PlaySoundButton();
        Time.timeScale = 1f;
        Debug.Log("Ir al menu de inicio");
        SceneManager.LoadSceneAsync("MainMenu");
    }
    public void LevelMenu()
    {
        PlaySoundButton();
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("Levels");
    }
    public void NextLv()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("Level2");
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

    private void ActivateMenu(object sender, EventArgs e)
    {
        fxSource.Stop();
        // mixer.GetFloat("VolMaster", out lastVolume);
        // mixer.SetFloat("VolMaster", -80); // Reduce volumen al mínimo
        // Destroy(gameObject);
        gameOverSound.Play();
        gameOverMenu.SetActive(true);
    }

    public void AddScore(int entryScore)
    {
        score += entryScore;
    }

    // Método para retornar el valor de score
    public int GetScore()
    {
        return score;
    }

    public void RestartGameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Debug.Log("Closing the game");
        Application.Quit();
    }
}
