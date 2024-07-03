// using System.Collections;
// using UnityEngine;
// using UnityEngine.UI;

// public class ImageTransition : MonoBehaviour
// {
//     public Transform parentTransform; // Transform del padre que contiene las imágenes
//     public float transitionTime = 2f; // Tiempo en segundos entre cada transición

//     private Image image1; // Imagen específica 1
//     private Image image2; // Imagen específica 2
//     private bool isImage1Active = true; // Indicador de imagen activa

//     void Start()
//     {
//         // Obtener las imágenes específicas dentro del objeto padre
//         image1 = parentTransform.Find("Image1").GetComponent<Image>();
//         image2 = parentTransform.Find("Image2").GetComponent<Image>();

//         // Mostrar mensajes de depuración sobre las imágenes encontradas
//         Debug.Log("Imagen 1 encontrada: " + image1.gameObject.name);
//         Debug.Log("Imagen 2 encontrada: " + image2.gameObject.name);

//         // Asegurarse de que al menos una imagen esté activa al inicio
//         image1.gameObject.SetActive(isImage1Active);
//         image2.gameObject.SetActive(!isImage1Active);

//         // Iniciar la rutina de transición de imágenes
//         StartCoroutine(TransitionImages());
//     }

//     IEnumerator TransitionImages()
//     {
//         while (true)
//         {
//             yield return new WaitForSeconds(transitionTime);

//             // Desactivar la imagen actual y activar la siguiente
//             image1.gameObject.SetActive(!isImage1Active);
//             image2.gameObject.SetActive(isImage1Active);

//             // Alternar el indicador de imagen activa
//             isImage1Active = !isImage1Active;
//         }
//     }
// }
