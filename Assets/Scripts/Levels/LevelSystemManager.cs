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

        public LevelData LevelData { get => levelData; }

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
                SaveLoadData.Instance.LoadData(userId, OnScoreLoaded);
                StartCoroutine(WaitForDataAndUpdateUI()); // Llamada al método definido correctamente
            }
            else
            {
                Debug.LogError("User ID not found. Cannot load level data.");
            }
        }

        // Callback para manejar el puntaje cargado
        private void OnScoreLoaded(int score)
        {
            Debug.Log("Score loaded: " + score);
            // Aquí puedes hacer algo con el puntaje cargado, si es necesario
        }

        public void ReloadDataForNewUser(int userId)
        {
            SaveLoadData.Instance.LoadData(userId, OnScoreLoaded);
            StartCoroutine(WaitForDataAndUpdateUI());
        }

        // Método que espera la carga de datos y actualiza la interfaz de usuario
        private IEnumerator WaitForDataAndUpdateUI()
        {
            yield return new WaitForSeconds(2f); // Ajusta este tiempo de espera según sea necesario
            Debug.Log("Updating UI with loaded data.");
            LevelUIManager.Instance.InitializeUI(); // Asegúrate de que LevelUIManager esté correctamente implementado
        }
    }
}
