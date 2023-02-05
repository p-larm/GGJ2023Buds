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
    private string presonName;

    private bool activated;

    private void Start() {
        activated = false;
    }

    public void ActivateWithInput(InputAction.CallbackContext context) {
        if(context.performed) {
            dialouge.StartDialogue();
            activated = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            dialouge.StartDialogue();
            activated = true;
        }
    }
}
