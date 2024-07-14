using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace LevelUnlockSystem
{
    public class GameUICar : MonoBehaviour
    {
        [SerializeField] private Image[] starsArray;            // Array of stars
        [SerializeField] private GameObject overPanel;          // Reference to over panel
        [SerializeField] private TextMeshProUGUI[] levelStatusText; // Array to store the child texts of the parent object
        [SerializeField] private Color lockColor, unlockColor;  // Reference to colors
        public int starCount = 3;                // Number of stars achieved

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("OnTriggerEnter2D called with " + other.name); // Debug log

            if (other.CompareTag("MainCar"))
            {
                Debug.Log("MainCar entered trigger");

                if (starCount > 0)                               // If star count is more than 0
                {
                    Debug.Log("StarCount " + starCount + " set to unlockColor");
                    // Change the text of all elements in levelStatusText
                    foreach (var textElement in levelStatusText)
                    {
                        textElement.text = "Level " + (LevelSystemManager.Instance.CurrentLevel + 1) + " Complete";
                    }
                    LevelSystemManager.Instance.LevelComplete(starCount);   // Send the information to LevelSystemManager
                }
                else                                             // Else only set the levelStatusText
                {
                    foreach (var textElement in levelStatusText)
                    {
                        textElement.text = "Level " + (LevelSystemManager.Instance.CurrentLevel + 1) + " Failed";
                    }
                }
                SetStar(starCount);                              // Set the stars based on starCount
                overPanel.SetActive(true);                       // Activate the over panel
            }
        }

        public void OkBtn(string nextLevel) // Method called by ok button
        {
            SceneManager.LoadScene(nextLevel);
        }

        private void SetStar(int starAchieved)
        {
            for (int i = 0; i < starsArray.Length; i++)             // Loop through entire star array
            {
                Debug.Log("Star " + i + " array " + starsArray.Length);

                if (i < starAchieved)
                {
                    starsArray[i].color = unlockColor;              // Set its color to unlockColor
                    Debug.Log("Star " + i + " set to unlockColor");
                }
                else
                {
                    starsArray[i].color = lockColor;                // Else set its color to lockColor
                    Debug.Log("Star " + i + " set to lockColor");
                }
            }
        }
    }
}
