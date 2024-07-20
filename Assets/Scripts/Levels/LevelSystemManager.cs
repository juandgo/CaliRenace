using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script holds the level data scriptable object and its Singleton and doesn't get deleted on scene change.
/// </summary>
namespace LevelUnlockSystem
{
    public class LevelSystemManager : MonoBehaviour
    {
        private static LevelSystemManager instance;                             // Instance variable
        public static LevelSystemManager Instance { get => instance; }          // Instance getter

        [Tooltip("Set the default Level data so when game start 1st time, this data will be saved")]
        [SerializeField] private LevelData levelData;

        public LevelData LevelData { get => levelData; }   // Getter

        private int currentLevel;                                               // Keep track of the current level player is playing
        public int CurrentLevel { get => currentLevel; set => currentLevel = value; }   // Getter and setter for currentLevel

        private void Awake()
        {
            if (instance == null)                                               // If instance is null
            {
                instance = this;                                                // Set this as instance
                DontDestroyOnLoad(gameObject);                                  // Make it DontDestroyOnLoad
            }
            else
            {
                Destroy(gameObject);                                            // Else destroy it
            }
        }

        private void Start()
        {
            if (SaveLoadData.Instance == null)
            {
                Debug.LogError("SaveLoadData instance is not initialized.");
            }
            else
            {
                SaveLoadData.Instance.Initialize();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SaveLoadData.Instance.SaveData();
            }
        }

        public void LevelComplete(int starAchieved)                             // Method called when player wins the level
        {
            levelData.levelItemArray[currentLevel].starAchieved = starAchieved;    // Save the stars achieved by the player in the level
            if (levelData.lastUnlockedLevel < (currentLevel + 1))
            {
                levelData.lastUnlockedLevel = currentLevel + 1;           // Change the lastUnlockedLevel to next level
                                                                          // And make the next level unlock true
                levelData.levelItemArray[levelData.lastUnlockedLevel].unlocked = true;
            }
        }
    }
}
