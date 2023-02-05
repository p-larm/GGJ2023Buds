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
    private string[] names;

    private bool activated;

    private void Start() {
        activated = false;
    }

    public void ActivateWithInput() {
        dialouge.InitializeAndStartDialogue(sentences, names);
        activated = true;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player") && !activated) {
            dialouge.InitializeAndStartDialogue(sentences, names);
            activated = true;
        }
    }
}
