using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nutrient : MonoBehaviour
{
    private bool triggered = false;

    private void OnTriggerEnter(Collider other) {
        if(!triggered) {
            if(other.gameObject.CompareTag("Player")) {
                other.gameObject.GetComponent<Bud>().CreateBud();
                triggered = true;
                Destroy(gameObject);
            }
        }
    }
}
