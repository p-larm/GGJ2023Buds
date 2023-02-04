using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Root : MonoBehaviour
{
    [SerializeField]
    private Billboard interactPopup;
    [SerializeField]
    private Color connectionColor;
    private SphereCollider sphereCollider;
    private LineRenderer lineRenderer;

    [SerializeField]
    private bool isOriginalRoot;
    private bool activated;
    private bool interacted;
    private int playerCount;
    private Bud rootBud;

    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        lineRenderer = GetComponent<LineRenderer>();
        activated = false;
        interacted = false;
        playerCount = 0;
    }

    public void Interact(InputAction.CallbackContext context) {
        if(context.performed) {
            if(activated && !interacted) {
                if(!isOriginalRoot) {
                    interacted = true;
                }
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(1, transform.position);
                lineRenderer.SetPosition(0, rootBud.GetRoot().transform.position);
                lineRenderer.startColor = connectionColor;
                lineRenderer.endColor = connectionColor;
                rootBud.SetRoot(gameObject.GetComponent<Rigidbody>());
                rootBud.SetCarryingNewRoot(true);
                TriggerPopup(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            if(!activated && !interacted) {
                activated = true;
                playerCount++;
                TriggerPopup(activated);
                rootBud = other.gameObject.GetComponent<Bud>().GetRootBud();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(!interacted) {
            if(other.gameObject.CompareTag("Player")) {
                playerCount--;
                if(playerCount == 0) {
                    activated = false;
                    TriggerPopup(activated);
                }
            }
        }
    }

    private void TriggerPopup(bool popup) {
        if(interactPopup != null) {
            if(popup) {
                // make popup
                interactPopup.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                interactPopup.TurnOn();
            } else {
                // remove popup
                interactPopup.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                interactPopup.TurnOff();
            }
        } else {
            Debug.Log("New Root: No billboard popup");
        }
    }

    public bool GetActivated() {
        return activated;
    }

    public Bud GetRootBud() {
        return rootBud;
    }
}
