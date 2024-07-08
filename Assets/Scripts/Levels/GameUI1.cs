using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace LevelUnlockSystem
{
    /// <summary>
    /// This is example script to show how to use the LevelSystemManager
    /// </summary>
    public class GameUI1 : MonoBehaviour
    {
        [SerializeField] private Image[] starsArray;            //array of stars
        [SerializeField] private Text levelStatusText;          //level status text
        [SerializeField] private GameObject overPanel;          //ref to over panel
        [SerializeField] private Color lockColor, unlockColor;  //ref to colors

    void OnTriggerEnter2D (Collider2D other)
    {
        int starCount =3;
	    if (other.CompareTag("MainCar"))
        {
            if (starCount > 0)                                  //if start count is more than 0
            {                                                   //set the levelStatusText
                levelStatusText.text = "Level " + (LevelSystemManager.Instance.CurrentLevel + 1) + " Complete";
                LevelSystemManager.Instance.LevelComplete(starCount);   //send the information to LevelSystemManager
            }
            else
            {
                                                                //else only set the levelStatusText
                levelStatusText.text = "Level " + (LevelSystemManager.Instance.CurrentLevel + 1) + " Failed";
            }
            SetStar(starCount);                                 //set the stars
            overPanel.SetActive(true);                
        }
	}
        public void OkBtn()                                     //method called by ok button
        {
            SceneManager.LoadScene(0);
        }

        /// <summary>
        /// Method to show number of stars achieved by the player for this perticular level
        /// </summary>
        /// <param name="starAchieved"></param>
        private void SetStar(int starAchieved)
        {
            for (int i = 0; i < starsArray.Length; i++)             //loop through entire star array
            {
                if (i < starAchieved)
                {
                    starsArray[i].color = unlockColor;              //set its color to unlockColor
                }
                else
                {
                    starsArray[i].color = lockColor;                //else set its color to lockColor
                }
            }
        }
    }
}