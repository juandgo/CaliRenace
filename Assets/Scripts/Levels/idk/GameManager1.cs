// using UnityEngine;
// using LevelUnlock;

// public class GameManager1 : MonoBehaviour
// {
//     public int userId;

//     private void Start()
//     {
//         // Asume que userId es inicializado apropiadamente antes de llamar a LoadData
//         SaveLoadData.Instance.LoadData(userId);
//     }

//     private void Update()
//     {
//         // Para este ejemplo, asumiremos que el nivel se carga automáticamente después de recibir los datos.
//         // Puedes ajustar este flujo según tus necesidades.
//         if (Input.GetKeyDown(KeyCode.L))
//         {
//             int currentLevel = SaveLoadData.Instance.GetCurrentLevel();
//             int nextLevel = SaveLoadData.Instance.GetNextUnlockedLevel();

//             Debug.Log("Current Level: " + currentLevel);
//             Debug.Log("Next Unlocked Level: " + nextLevel);
//         }
//     }
// }
