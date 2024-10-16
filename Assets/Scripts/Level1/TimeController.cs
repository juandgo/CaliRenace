using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    [SerializeField] private float maxTime;
    [SerializeField] private Slider slider;
    [SerializeField] public AudioSource timerSound;
    [SerializeField] public AudioSource winSound;
    [SerializeField] private Level1 lv1;
    public GameObject winCup;


    private float actualTime;

    private bool activatedTime = false;

    // private void Start(){
    //     activateTimer();
    // }

    private void Update()
    {
        if (activatedTime)
        {
            ChangeCounter();
        }
    }

    private void ChangeCounter()
    {
        actualTime -= Time.deltaTime;

        if (actualTime >= 0)
        {
            slider.value = actualTime;
        }

        if (actualTime <= 0)
        {
            Debug.Log("Derrota");
            ChangeTimer(false);
            timerSound.Stop();
            lv1.fxSource.Play();
        }
    }

    private void ChangeTimer(bool status)
    {
        activatedTime = status;
    }

    public void activateTimer()
    {
        actualTime = maxTime;
        slider.maxValue = maxTime;
        ChangeTimer(true);
        timerSound.Play();
        lv1.fxSource.Stop();
        // winCup.SetActive(true);

    }

public void deactivateTimer()
{
    timerSound.Stop();
    if (actualTime >= 0)
    {
        Debug.Log("You win");
        winCup.SetActive(true);
        StartCoroutine(PlayWinAndFxSounds());
    }
    ChangeTimer(false);
}

private IEnumerator PlayWinAndFxSounds()
{
    // Reproduce el sonido de winCup
    winSound.Play();

    // Espera a que termine winSound antes de reproducir fxSource
    // yield return new WaitForSeconds(winSound.clip.length);
    yield return new WaitForSeconds(1.5f);

    // Reproduce fxSource después de que termine winSound
    lv1.fxSource.Play();
}
}
