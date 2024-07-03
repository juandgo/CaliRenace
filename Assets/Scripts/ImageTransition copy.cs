using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageTransitions : MonoBehaviour
{
    // public Image UIImage;
    public Image[] images;
    public float transitionTime = 2f;

    private int currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TransitionRoutine());
    }

    IEnumerator TransitionRoutine()
    {
        while (currentIndex < images.Length)
        {
            ShowImageAndText(currentIndex);
            yield return new WaitForSeconds(transitionTime);
            HideAll();
            currentIndex++;
        }
    }
    void ShowImageAndText(int index)
    {
        images[index].gameObject.SetActive(true);
    }
    void HideAll()
    {
        foreach (var img in images)
        {
            img.gameObject.SetActive(false);
        }
    }
   
}
