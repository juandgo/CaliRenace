using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LevelUnlock;


namespace LevelUnlockSystem
{
    public class GameUICar : MonoBehaviour
    {
        [SerializeField] private Image[] starsArray;            //array of stars
        [SerializeField] private GameObject overPanel;          //ref to over panel
        [SerializeField] private TextMeshProUGUI[] levelStatusText; // Arreglo para almacenar los textos hijos del objeto padre
        [SerializeField] private Color lockColor, unlockColor;  //ref to colors
        // public int starCount = 3;                //number of stars achieved
        public int levelId=5;
        public int score;
        private int userId;

        void Start()
        {
            // Obtén el userId de PlayerPrefs
            userId = PlayerPrefs.GetInt("accountUserId", -1);
            if (userId == -1)
            {
                Debug.LogError("No se encontró el ID de usuario guardado.");
            }
        }
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("MainCar"))
            {
                // Debug.Log("MainCar");
                // if (starCount > 0)                               // if star count is more than 0
                // {
                //     // Debug.Log("StarCount " + starCount + " set to unlockColor");
                //     // Cambiar el texto de todos los elementos en levelStatusText
                //     foreach (var textElement in levelStatusText)
                //     {
                //         // textElement.text = "Level " + (LevelSystemManager.Instance.CurrentLevel + 1) + " Complete";
                //     }
                //     // LevelSystemManager.Instance.LevelComplete(starCount);   // send the information to LevelSystemManager
                // }
                // else                                             // else only set the levelStatusText
                // {
                //     foreach (var textElement in levelStatusText)
                //     {
                //         // textElement.text = "Level " + (LevelSystemManager.Instance.CurrentLevel + 1) + " Failed";
                //     }
                // }
                // SetStar(starCount);                              //set the stars based on starCount
                overPanel.SetActive(true);                       //activate the over panel
            }
        }

        public void OkBtn()//JD
        {
            if (SaveLoadData.Instance != null)
            {
                // Debug.Log($"USER ID {userId}");
                SaveLoadData.Instance.SaveData(userId, levelId, "1", 3);
            }
            else
            {
                Debug.Log("SaveLoadData.Instance is null");
            }
            SceneManager.LoadScene("Levels");
        }

        // private void SetStar(int starAchieved)
        // {
        //     for (int i = 0; i < starsArray.Length-1; i++)             //loop through entire star array
        //     {
        //         Debug.Log("Star " + i+" array "+starsArray.Length);

        //         if (i < starAchieved)
        //         {
        //             starsArray[i].color = unlockColor;              //set its color to unlockColor
        //             // Debug.Log("Star " + i + " set to unlockColor");
        //         }
        //         else
        //         {
        //             starsArray[i].color = lockColor;                // else set its color to lockColor
        //             // Debug.Log("Star " + i + " set to lockColor");
        //         }
        //     }
        // }
    }
}
