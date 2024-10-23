using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using UnityEngine.SceneManagement;
using System;
using Newtonsoft.Json.Linq;

namespace LevelUnlock
{
    public class SaveLoadData : MonoBehaviour
    {
        private static SaveLoadData instance;
        private string baseURL = "http://localhost/www/UnityLoginLogoutRegister/save_load_data.php";
        // private string baseURL = "http://localhost/www/GameBuiltedWeb/UnityLoginLogoutRegister/save_load_data.php";
        // private string baseURL = "http://GameBuiltedWeb/UnityLoginLogoutRegister/save_load_data.php";

        public static SaveLoadData Instance { get => instance; }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Initialize(int userId)
        {
            if (PlayerPrefs.GetInt("GameStartFirstTime") == 1)
            {
                LoadData(userId, null); // Carga los datos si ya ha iniciado antes
            }
            else
            {
                PlayerPrefs.SetInt("GameStartFirstTime", 1); // Primera vez que se inicia el juego
            }
        }

        private void OnApplicationQuit()
        {
            // ClearData();
        }

        public void SaveData(int userId, int levelId, string completionStatus, int score)
        {
            StartCoroutine(SaveDataCoroutine(userId, levelId, completionStatus, score));
        }

        private IEnumerator SaveDataCoroutine(int userId, int levelId, string completionStatus, int score)
        {
            WWWForm form = new WWWForm();
            form.AddField("action", "save");
            form.AddField("userId", userId);
            form.AddField("levelId", levelId);
            form.AddField("completionStatus", completionStatus);
            form.AddField("score", score);

            UnityWebRequest www = UnityWebRequest.Post(baseURL, form);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error saving data: " + www.error);
            }
            else
            {
                string jsonResponse = www.downloadHandler.text;
                jsonResponse = "{\"levels\":" + jsonResponse + "}";
                Debug.Log("SAVE: " + jsonResponse);
                LevelDataWrapper wrap = JsonUtility.FromJson<LevelDataWrapper>(jsonResponse);

                if (wrap != null && wrap.levels != null)
                {
                    LevelSystemManager.Instance.LevelData.levelItemArray = wrap.levels;

                    int lastCompletedLevel = wrap.levels
                        .Where(level => level.completion_status == "1")
                        .Select(level => int.Parse(level.level_id))
                        .DefaultIfEmpty(0)
                        .Max();

                    int nextLevelToUnlock = lastCompletedLevel + 1;
                    LevelSystemManager.Instance.LevelData.lastUnlockedLevel = nextLevelToUnlock;

                    // Actualizar la UI
                    // LevelUIManager.Instance.UpdateUI(wrap.levels, nextLevelToUnlock);
                    LevelUIManager.Instance.InitializeUI();
                    
                }
                else
                {
                    Debug.LogError("Failed to parse JSON response.");
                }
            }
        }

        public void LoadData(int userId, Action<int> onScoreLoaded)
        {
            StartCoroutine(LoadDataCoroutine(userId, onScoreLoaded));
        }

        private IEnumerator LoadDataCoroutine(int userId, Action<int> onScoreLoaded)
        {
            WWWForm form = new WWWForm();
            form.AddField("action", "load");
            form.AddField("userId", userId);

            UnityWebRequest www = UnityWebRequest.Post(baseURL, form);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error loading data: " + www.error);
            }
            else
            {
                string jsonResponse = www.downloadHandler.text;
                jsonResponse = "{\"levels\":" + jsonResponse + "}";
                LevelDataWrapper wrapper = JsonUtility.FromJson<LevelDataWrapper>(jsonResponse);

                if (wrapper != null && wrapper.levels != null)
                {
                    // Actualiza los datos del sistema de niveles
                    LevelSystemManager.Instance.LevelData.levelItemArray = wrapper.levels;

                    int lastCompletedLevel = wrapper.levels
                        .Where(level => level.completion_status == "1")
                        .Select(level => int.Parse(level.level_id))
                        .DefaultIfEmpty(0)
                        .Max();

                    int nextLevelToUnlock = lastCompletedLevel + 1;
                    LevelSystemManager.Instance.LevelData.lastUnlockedLevel = nextLevelToUnlock;

                    // Actualizar la UI
                    // LevelUIManager.Instance.UpdateUI(wrapper.levels, nextLevelToUnlock);

                    // Devuelve el puntaje si se requiere
                    if (onScoreLoaded != null)
                    {
                        LevelItem currentLevel = wrapper.levels.FirstOrDefault(level => level.level_id == "1");
                        int score = 0;

                        if (currentLevel != null && int.TryParse(currentLevel.score, out score))
                        {
                            onScoreLoaded.Invoke(score);
                        }
                        else
                        {
                            onScoreLoaded.Invoke(0);  // Puntaje 0 si no se encuentra
                        }
                    }
                }
                else
                {
                    Debug.LogError("Failed to parse JSON response.");
                }
            }
        }

        public void ClearData()
        {
            Debug.Log("Data Cleared");

            var levelData = LevelSystemManager.Instance.LevelData;
            levelData.lastUnlockedLevel = 0;
            for (int i = 0; i < levelData.levelItemArray.Length; i++)
            {
                levelData.levelItemArray[i].completion_status = "0";
                levelData.levelItemArray[i].score = "0";
            }

            int userId = PlayerPrefs.GetInt("accountUserId", -1);
            if (userId != -1)
            {
                SaveData(userId, 1, "0", 0); // Reset data
            }

            PlayerPrefs.SetInt("GameStartFirstTime", 0);
            PlayerPrefs.DeleteKey("accountUserId");
        }

        public int GetCurrentLevel()
        {
            return LevelSystemManager.Instance.CurrentLevel;
        }

        public int GetNextUnlockedLevel()
        {
            return LevelSystemManager.Instance.CurrentLevel + 1; // O ajusta según tu lógica
        }
    }

    [System.Serializable]
    public class LevelData
    {
        public int lastUnlockedLevel;
        public LevelItem[] levelItemArray;
    }

    [System.Serializable]
    public class LevelItem
    {
        public string level_id;
        public string level_name;
        public string completion_status;
        public string score;
    }

    [System.Serializable]
    public class LevelDataWrapper
    {
        public LevelItem[] levels;
    }
}
