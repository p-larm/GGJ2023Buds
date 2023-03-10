using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField]
    private Transform respawnPoint;
    [SerializeField]
    private bool isDialougeTrigger;
    [SerializeField]
    private DialougeTrigger dialougeTrigger;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            other.gameObject.GetComponent<Bud>().Respawn(respawnPoint.position);
            if(isDialougeTrigger) {
                dialougeTrigger.ActivateWithInput();
            }
        }
    }
}
