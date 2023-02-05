using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialougeTrigger : MonoBehaviour
{
    [SerializeField]
    private Dialogue dialouge;

    [SerializeField]
    private string[] sentences;

    [SerializeField]
<<<<<<< Updated upstream
    private string presonName;
=======
    private string[] names;
>>>>>>> Stashed changes

    private bool activated;

    private void Start() {
        activated = false;
    }

<<<<<<< Updated upstream
    public void ActivateWithInput(InputAction.CallbackContext context) {
        if(context.performed) {
            dialouge.StartDialogue();
            activated = true;
        }
=======
    public void ActivateWithInput() {
        dialouge.InitializeAndStartDialogue(sentences, names);
        activated = true;
>>>>>>> Stashed changes
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
<<<<<<< Updated upstream
            dialouge.StartDialogue();
=======
            dialouge.InitializeAndStartDialogue(sentences, names);
>>>>>>> Stashed changes
            activated = true;
        }
    }
}
