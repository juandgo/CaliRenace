using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;
using System;

public class Level1 : MonoBehaviour
{
    [SerializeField] private GameObject btnPause;
    [SerializeField] private GameObject pauseMenu;
    private bool pausedGame = false;

    [Header("Sounds")]
    public AudioMixer mixer;
    public AudioSource fxSource;
    public AudioClip clickSound;

    [Header("Game Over Menu")]
    [SerializeField] private GameObject gameOverMenu;
    private PlayerMovement playerLife;

    public TextMeshProUGUI textMeshScore;
    public TextMeshProUGUI textMeshLevel;
    private float score;
    private string level = "1";

    private UnityLoginRegister unityLginRegisterInstance;
    
    private void Start()
    {
        unityLginRegisterInstance = gameObject.AddComponent<UnityLoginRegister>();
        // username = unityLginRegisterInstance.GetName();
        playerLife = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
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
    }

    public void Pause()
    {
        pausedGame = true;
        Time.timeScale = 0f;
        btnPause.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        pausedGame = false;
        Time.timeScale = 1f;
        btnPause.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void Restart()
    {
        pausedGame = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("Ir al menu de inicio");
        SceneManager.LoadSceneAsync("MainMenu");
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
        gameOverMenu.SetActive(true);
    }

    public void AddScore(float entryScore)
    {
        score += entryScore;
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
