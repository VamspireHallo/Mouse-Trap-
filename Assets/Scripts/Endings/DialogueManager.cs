using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] private GameObject dialogueBox;  // Dialogue UI box
    [SerializeField] private Text dialogueText;       // Text element for dialogue
    [SerializeField] private float typingSpeed = 0.05f; // Speed of text typing effect

    private Queue<string> dialogueQueue; // Queue to hold dialogue lines
    private bool isDialogueActive = false;

    private void Awake()
    {
        // Singleton pattern to ensure one instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        dialogueQueue = new Queue<string>();
        if (dialogueBox != null) dialogueBox.SetActive(false); // Hide dialogue box initially
    }

    public void StartDialogue(List<string> dialogueLines)
    {
        if (dialogueLines == null || dialogueLines.Count == 0) return;

        dialogueQueue.Clear(); // Clear any existing dialogue
        foreach (string line in dialogueLines)
        {
            dialogueQueue.Enqueue(line);
        }

        isDialogueActive = true;
        dialogueBox.SetActive(true);
        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        string nextLine = dialogueQueue.Dequeue();
        StopAllCoroutines(); // Stop any ongoing typing effects
        StartCoroutine(TypeDialogue(nextLine));
    }

    private IEnumerator TypeDialogue(string line)
    {
        dialogueText.text = ""; // Clear text before typing
        foreach (char letter in line)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        dialogueBox.SetActive(false); // Hide dialogue box
    }

    private void Update()
    {
        // Allow player to proceed to the next line of dialogue
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Z))
        {
            DisplayNextLine();
        }
    }
}
