// using UnityEngine;
// using System.Collections;

// namespace LevelUnlock
// {
//     public class LevelManager : MonoBehaviour
//     {
//         public int levelId;
//         public int score;
//         private int userId;
//         // Método llamado al completar un nivel
//         public void OnLevelCompleted(int completedLevelId, int score)
//         {
//             // Llama a SaveData para actualizar la información en el servidor
//             // SaveLoadData.Instance.SaveData(userId, completedLevelId, true, score);
//             SaveLoadData.Instance.SaveData(userId, completedLevelId, true, score);

//             // Actualiza la UI o lógica de niveles aquí
//             UpdateLevelUI();
//         }

//         private void UpdateLevelUI()
//         {
//             // Código para actualizar la interfaz de usuario del selector de niveles
//             // Asegúrate de que se llame después de que los datos se hayan actualizado
//             int currentLevel = SaveLoadData.Instance.GetCurrentLevel();
//             int nextUnlockedLevel = SaveLoadData.Instance.GetNextUnlockedLevel();

//             // Lógica para habilitar o deshabilitar botones de niveles en la UI
//         }


//         // private void Start()
//         // {
//         //     userId = PlayerPrefs.GetInt("accountUserId"); // Asegúrate de que se haya guardado correctamente
//         //     StartCoroutine(LoadUserData());
//         // }

//         // private IEnumerator LoadUserData()
//         // {
//         //     // Asegúrate de que SaveLoadData esté inicializado
//         //     while (SaveLoadData.Instance == null)
//         //     {
//         //         yield return null;
//         //     }

//         //     // Cargar los datos del usuario
//         //     SaveLoadData.Instance.LoadData(userId);

//         //     // Esperar un tiempo para asegurarse de que los datos se carguen
//         //     yield return new WaitForSeconds(2);

//         //     // Obtener el nivel actual y el siguiente nivel desbloqueado
//         //     int currentLevel = SaveLoadData.Instance.GetCurrentLevel();
//         //     int nextUnlockedLevel = SaveLoadData.Instance.GetNextUnlockedLevel();

//         //     Debug.Log("Current Level: " + currentLevel);
//         //     Debug.Log("Next Unlocked Level: " + nextUnlockedLevel);

//         //     // Aquí puedes proceder con la lógica de la escena, como mostrar los niveles desbloqueados
//         //     // Usa SaveLoadData.Instance.GetLevelItems() para obtener los datos de los niveles
//         // }
//     }
// }
