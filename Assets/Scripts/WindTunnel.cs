using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTunnel : MonoBehaviour
{
    [SerializeField]
    private float windForce;
    [SerializeField] [Range(0,1)]
    private float windChangeAmt;
    [SerializeField]
    private List<Vector2> windDirections;
    private Vector2 targetDirection;
    private Vector2 currentDirection;
    private float xDir;
    private float xDirInc;
    private float yDir;
    private float yDirInc;
    private bool active;

    [SerializeField]
    private List<PlayerMovement> budList;

    // Start is called before the first frame update
    void Start()
    {
        budList = new List<PlayerMovement>();
        active = true;
        StartCoroutine(WindDirection());
    }

    private void Update() {
        Vector3 force = new Vector3(currentDirection.x, 0, currentDirection.y) * windForce;
        foreach(PlayerMovement bud in budList) {
            bud.AddForce(force);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            Debug.Log("Player Enter");
            budList.Add(other.gameObject.GetComponent<PlayerMovement>());
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            Debug.Log("Player Exit");
            for(int i = 0; i < budList.Count; i++) {
                if(budList[i] == other.gameObject.GetComponent<PlayerMovement>()) {
                    budList.RemoveAt(i);
                    i = budList.Count;
                }
            }
        }
    }

    private IEnumerator WindDirection() {
        while(active) {
            foreach(Vector2 direction in windDirections) {
                targetDirection = direction;
                while(Vector2.Angle(currentDirection, targetDirection) > 1f) {
                    currentDirection = Vector2.Lerp(currentDirection, targetDirection, windChangeAmt);
                    yield return null;
                }
                currentDirection = targetDirection;
                yield return new WaitForSeconds(5);
            }
        }
    }
}
