using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// This script handles level button functions
/// </summary>
namespace LevelUnlock
{
    public class LevelButtonScript : MonoBehaviour
    {
        [SerializeField] private GameObject lockObj, unlockObj;     // Ref to lock and unlock gameobjects
        [SerializeField] private Text levelIndexText;               // Ref to text which indicates the level number
        [SerializeField] private Color lockColor, unlockColor;      // Color references
        [SerializeField] private Button btn;                        // Ref to hold button component
        [SerializeField] private GameObject activeLevelIndicator;
        [SerializeField] public AudioSource clickSound;

        private int levelIndex;                                     // Holds the level index this particular button specifies

        private void Start()
        {
            btn.onClick.AddListener(OnClick);                      // Add listener to the button
        }

        public void SetLevelButton(LevelItem value, int index, bool activeLevel)
        {
            // Check if this is the second level (index 1)
            if (index == 0)
            {
                // Force the second level to be unlocked
                value.completion_status = "1";
            }

            string completionStatus = value.completion_status; // Convert string to bool
            // Debug.Log("completionStatus: "+ completionStatus);
            if (completionStatus == "1") // Use the converted bool
            {
                activeLevelIndicator.SetActive(activeLevel);
                levelIndex = index + 1;                             // Set levelIndex, Note: We add 1 because array starts from 0 and level index starts from 1
                btn.interactable = true;                            // Make button interactable
                lockObj.SetActive(false);                           // Deactivate lockObj
                unlockObj.SetActive(true);                          // Activate unlockObj
                levelIndexText.text = levelIndex.ToString();        // Set levelIndexText text
            } 
            else
            {
                btn.interactable = false;                           // Make button non-interactable
                lockObj.SetActive(true);                            // Activate lockObj
                unlockObj.SetActive(false);                         // Deactivate unlockObj
            }
        }

        private void OnClick()                                      // Method called by button
        {
            LevelSystemManager.Instance.CurrentLevel = levelIndex - 1;  // Set the CurrentLevel, we subtract 1 as level data array starts from 0
            SceneManager.LoadScene("Level" + levelIndex);           // Load the level
            clickSound.Play();                                      // Play click sound
        }
    }
}
