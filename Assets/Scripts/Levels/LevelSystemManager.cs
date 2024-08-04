using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace LevelUnlock
{
    
    public class LevelSystemManager : MonoBehaviour
    {
        public static LevelSystemManager Instance { get; private set; }
        public int CurrentLevel { get; set; }
        public Button[] levelButtons;

        [SerializeField] private LevelData levelData;

        public LevelData LevelData{ get => levelData; }
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            int userId = PlayerPrefs.GetInt("accountUserId", -1);
            if (userId != -1)
            {
                SaveLoadData.Instance.LoadData(userId);
                StartCoroutine(WaitForDataAndUpdateUI());
            }
            else
            {
                Debug.LogError("User ID not found. Cannot load level data.");
            }
        }

        private IEnumerator WaitForDataAndUpdateUI()
        {
            yield return new WaitForSeconds(2f); // Adjust this wait time as needed
            Debug.Log("Updating UI with loaded data.");
            LevelUIManager.Instance.InitializeUI();
        }

    }
}
