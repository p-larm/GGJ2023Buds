using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRoot : MonoBehaviour
{
    [SerializeField]
    private Billboard interactPopup;
    private SphereCollider sphereCollider;

    private bool activated;
    private int playerCount;

    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        activated = false;
        playerCount = 0;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            if(!activated) {
                activated = true;
                playerCount++;
                TriggerPopup(activated);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            playerCount--;
            if(playerCount == 0) {
                activated = false;
                TriggerPopup(activated);
            }
        }
    }

    private void TriggerPopup(bool popup) {
        if(interactPopup != null) {
            if(popup) {
                // make popup
                interactPopup.TurnOn();
            } else {
                // remove popup
                interactPopup.TurnOff();
            }
        } else {
            Debug.Log("New Root: No billboard popup");
        }
    }
}
