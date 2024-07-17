using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnBack : MonoBehaviour
{
    // Start is called before the first frame update
    public void BackToMainMenu(string levelName)
    {
        Debug.Log("Ir al menu de inicio");
        SceneManager.LoadScene(levelName);
        // PlaySoundButton();
    }
}
