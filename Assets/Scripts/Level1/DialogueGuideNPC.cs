using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueGuideNPC : MonoBehaviour
{

    [SerializeField] private GameObject dialogueMark, dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField] private GameObject player;
    public MateoPlayer mateoPlayer;
    // [SerializeField] private GameObject ExplaningEfect;
    //   [SerializeField] private AudioSource exclamationSound;

    private bool isPlayerInRange, didDialogueStart;
    [Header("Animation")]
    private Animator animator;
    private float typingTime = 0.05f;
    private int lineIndex;
    public Rigidbody2D playerRigidbody;
    void Start()
    {
        animator = GetComponent<Animator>();
        mateoPlayer = player.GetComponent<MateoPlayer>();
    }

    void Update()
    {
        // FlipTowardsMateoPlayer();
        // if(isPlayerInRange && Input.GetButtonDown("Submit")){
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.G))
        {
            FlipTowardsMateoPlayer();

            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        animator.SetBool("isExplaning", didDialogueStart);
        dialoguePanel.SetActive(true);
        dialogueMark.SetActive(false);
        lineIndex = 0;
        mateoPlayer.enabled = false;
        // mateoPlayer.GetComponent<Animator>().enabled = false;
        mateoPlayer.GetComponent<Animator>().Play("MateoRun");
        // mateoPlayer.GetComponent<Animator>().Play("MateoFight");
        // Congelar movimiento
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        // Time.timeScale = 0f;
        StartCoroutine(ShowLine());
    }

    private void FlipTowardsMateoPlayer()
    {
        // Obtener la dirección hacia el mateoPlayer
        Vector2 direction = mateoPlayer.transform.position - transform.position;

        // Voltear el NPC según la dirección del mateoPlayer
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Mirar a la derecha
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1); // Mirar a la izquierda
        }
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            animator.SetBool("isExplaning", didDialogueStart);
            mateoPlayer.enabled = true;
            // Descongelar movimiento
            playerRigidbody.constraints = RigidbodyConstraints2D.None;
            playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

            // Time.timeScale = 0f;

            dialoguePanel.SetActive(false);
            dialogueMark.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            dialogueMark.SetActive(true);
            // exclamationSound.Play();
            // Debug.Log("Se puede iniciar un dialogo");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogueMark.SetActive(false);
            // Debug.Log("No se puede iniciar un dialogo");
        }
    }
}
