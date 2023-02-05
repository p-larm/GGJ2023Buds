using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
<<<<<<< Updated upstream
=======
using UnityEngine.UI;
>>>>>>> Stashed changes

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textComponent;
    [SerializeField]
<<<<<<< Updated upstream
    private string[] lines;
    [SerializeField]
    private float textSpeed;
    [SerializeField]
    private SpriteRenderer personImage;
=======
    private TextMeshProUGUI nameTextComponent;
    [SerializeField]
    private RawImage spriteRenderer;
    [SerializeField]
    private Texture2D[] sprites;
    [SerializeField]
    private string[] lines;
    [SerializeField]
    private string[] names;
    [SerializeField]
    private float textSpeed;
>>>>>>> Stashed changes


    private int index;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
<<<<<<< Updated upstream
        StartDialogue();
=======
        gameObject.SetActive(false);
>>>>>>> Stashed changes
    }

    public void DialougeInput(InputAction.CallbackContext context) {
        if(context.performed) {
            if (textComponent.text == lines[index]) {
                NextLine();
            } else {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

<<<<<<< Updated upstream
    public void StartDialogue(){
        index = 0;
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine() {
=======
    private void StartDialogue(){
        index = 0;
        textComponent.text = "";
        gameObject.SetActive(true);
        StartCoroutine(TypeLine());
    }

    public void InitializeAndStartDialogue(string[] newLines, string[] newNames) {
        SetLines(newLines);
        SetNames(newNames);
        StartDialogue();
    }

    private IEnumerator TypeLine() {
        nameTextComponent.text = names[index];
        if(names[index] == "Bud") {
            spriteRenderer.texture = sprites[0];
        } else {
            spriteRenderer.texture = sprites[1];
        }

>>>>>>> Stashed changes
        foreach(char c in lines[index].ToCharArray()) {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void NextLine() {
        if(index < lines.Length - 1) {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        } else {
            gameObject.SetActive(false);
        }
    }
<<<<<<< Updated upstream
=======

    public void SetLines(string[] newLines) {
        lines = newLines;
    }

    public void SetNames(string[] newNames) {
        names = newNames;
    }
>>>>>>> Stashed changes
}
