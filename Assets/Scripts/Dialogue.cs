using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private Text textComponent;
    [SerializeField]
    private Text nameTextComponent;
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


    private int index;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        gameObject.SetActive(false);
        StartDialogue();
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

    public void SetLines(string[] newLines) {
        lines = newLines;
    }

    public void SetNames(string[] newNames) {
        names = newNames;
    }
}
