using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour{

    [SerializeField] private GameObject dialogueMark, dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    //   [SerializeField] private AudioSource exclamationSound;

    private bool isPlayerInRange, didDialogueStart;
    private float typingTime = 0.05f;
    private int lineIndex;

    void Update() {
        // if(isPlayerInRange && Input.GetButtonDown("Submit")){
        if(isPlayerInRange && Input.GetKeyDown(KeyCode.G)){

            if(!didDialogueStart){
                StartDialogue();
            }else if(dialogueText.text == dialogueLines[lineIndex] ){
                NextDialogueLine();
            }else{
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
        }        
    }

    private void StartDialogue() {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        dialogueMark.SetActive(false);
        lineIndex = 0;
        // Time.timeScale = 0f;
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine() {
        lineIndex++;
        if(lineIndex < dialogueLines.Length){
            StartCoroutine(ShowLine());
        }else{
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            dialogueMark.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    private IEnumerator ShowLine(){
        dialogueText.text = string.Empty;

        foreach(char ch in dialogueLines[lineIndex]){
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")){
            isPlayerInRange = true;
            dialogueMark.SetActive(true);
            // exclamationSound.Play();
            // Debug.Log("Se puede iniciar un dialogo");
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")){
            isPlayerInRange = false;
            dialogueMark.SetActive(false);
            // Debug.Log("No se puede iniciar un dialogo");
        }
    }
}
