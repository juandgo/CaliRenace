using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageTransition : MonoBehaviour
{
    public Image UIImage;
    // Start is called before the first frame update
    void Start()
    {
        UIImage = GameObject.Find("ChangingImage").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp("q")){
            UIImage.sprite = Resources.Load<Sprite>("Sprites/cali1");
        }     
        if(Input.GetKeyDown("w")){
            UIImage.sprite = Resources.Load<Sprite>("Sprites/cali2");
        }
    }
}
