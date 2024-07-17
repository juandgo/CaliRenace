using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasButton : MonoBehaviour
{
    public void Menu()
    {
        Debug.Log("Ir al menu de inicio");
        SceneManager.LoadScene(0);
    }
}
