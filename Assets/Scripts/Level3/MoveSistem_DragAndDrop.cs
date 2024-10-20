using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MoveSistem_DragAndDrop : MonoBehaviour
{
    public GameObject correctForm;
    private bool moving;
    private bool finish;
    private float startPosX, startPosY;

    public AudioSource fxSource;
    public AudioClip clickSound;

    //private float startPosX;
    //private float startPosY;

    private Vector3 resetPosition;

    void Start()
    {
        resetPosition = this.transform.localPosition;
    }


    void Update()
    {
        if (!finish)
        {
            if (moving)
            {
                Vector3 mousePos;
                mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, this.gameObject.transform.localPosition.z);
            }
        }

    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            moving = true;
        }

    }

    private void OnMouseUp()
    {
        moving = false;


        if (Mathf.Abs(this.transform.localPosition.x - correctForm.transform.localPosition.x) <= 0.5f &&
            Mathf.Abs(this.transform.localPosition.y - correctForm.transform.localPosition.y) <= 0.5f)
        {
            this.transform.position = new Vector3(correctForm.transform.position.x, correctForm.transform.position.y, correctForm.transform.position.z);
            finish = true;

            GameObject.Find("PointHandler").GetComponent<WinScriptLevel3>().AddPoints();
            PlaySoundButton();
        }
        else
        {
            //return to its start position
            this.transform.localPosition = new Vector3(resetPosition.x, resetPosition.y, resetPosition.z);
        }
    }

    public void PlaySoundButton()
    {
        fxSource.PlayOneShot(clickSound);
    }

}
