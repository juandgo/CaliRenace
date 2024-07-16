using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    public Animator creditsAnimator;

    void Start()
    {
        creditsAnimator.SetTrigger("StartCredits");
    }

    public void PlayCredits()
    {
        creditsAnimator.SetTrigger("StartCredits");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Levels");
        }
    }

}
