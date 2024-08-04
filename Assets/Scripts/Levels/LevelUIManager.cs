using UnityEngine;

namespace LevelUnlock
{
    public class LevelUIManager : MonoBehaviour
    {
        private static LevelUIManager instance; //instance variable
        public static LevelUIManager Instance { get => instance; } //instance getter

        [SerializeField] private LevelButtonScript levelBtnPrefab; //ref to LevelButton prefab
        [SerializeField] private Transform levelBtnGridHolder; //ref to grid holder

        private void Start()
        {
            InitializeUI();
        }

        private void Awake()
        {
            if (instance == null) //if instance is null
            {
                instance = this; //set this as instance
            }
            else
            {
                Destroy(gameObject); //else destroy it
            }
        }

        public void InitializeUI()
        {
            // Clear existing buttons
            for (int i = levelBtnGridHolder.childCount - 1; i >= 0; i--)
            {
                Destroy(levelBtnGridHolder.GetChild(i).gameObject);
            }

            LevelItem[] levelItemsArray = LevelSystemManager.Instance.LevelData.levelItemArray; //get the level data array

            for (int i = 0; i < levelItemsArray.Length; i++) //loop through entire array
            {
                LevelButtonScript levelButton = Instantiate(levelBtnPrefab, levelBtnGridHolder); //create button for each element in array
                bool isUnlocked = i <= LevelSystemManager.Instance.LevelData.lastUnlockedLevel; // Determine if the level is unlocked
                levelButton.SetLevelButton(levelItemsArray[i], i, isUnlocked);
            }
        }
    }
}
